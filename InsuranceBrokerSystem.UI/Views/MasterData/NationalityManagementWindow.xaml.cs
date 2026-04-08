using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace InsuranceBrokerSystem.UI.Views.MasterData
{
    public partial class NationalityManagementWindow : Window
    {
        public ObservableCollection<Nationality> Nationalities { get; set; }
        public Nationality SelectedNationality { get; private set; }

        private static List<Nationality> _nationalities = new List<Nationality>
        {
            new Nationality { Id = 1, Name = "Saudi Arabian", Code = "SA", Description = "Kingdom of Saudi Arabia" },
            new Nationality { Id = 2, Name = "United States", Code = "US", Description = "United States of America" },
            new Nationality { Id = 3, Name = "United Kingdom", Code = "GB", Description = "United Kingdom" },
            new Nationality { Id = 4, Name = "United Arab Emirates", Code = "AE", Description = "United Arab Emirates" },
            new Nationality { Id = 5, Name = "Egypt", Code = "EG", Description = "Arab Republic of Egypt" }
        };

        public NationalityManagementWindow()
        {
            InitializeComponent();
            Nationalities = new ObservableCollection<Nationality>();
            lstNationalities.ItemsSource = Nationalities;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                Nationalities.Clear();
                foreach (var item in _nationalities.OrderBy(x => x.Name))
                {
                    Nationalities.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading nationalities: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstNationalities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedNationality = lstNationalities.SelectedItem as Nationality;
            
            if (SelectedNationality != null)
            {
                txtName.Text = SelectedNationality.Name;
                txtCode.Text = SelectedNationality.Code;
                txtDescription.Text = SelectedNationality.Description;
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnAdd.IsEnabled = false;
            }
            else
            {
                ClearForm();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please enter a name and country code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newNationality = new Nationality
            {
                Name = txtName.Text.Trim(),
                Code = txtCode.Text.Trim().ToUpper(),
                Description = txtDescription.Text.Trim(),
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "CurrentUser"
            };

            try
            {
                newNationality.Id = _nationalities.Count > 0 ? _nationalities.Max(x => x.Id) + 1 : 1;
                
                if (_nationalities.Any(x => x.Name.Equals(newNationality.Name, StringComparison.OrdinalIgnoreCase) || 
                                           x.Code.Equals(newNationality.Code, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("A nationality with this name or code already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _nationalities.Add(newNationality);
                Nationalities.Add(newNationality);
                ClearForm();
                MessageBox.Show("Nationality added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding nationality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNationality == null || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Please select a nationality and enter a name and country code.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedNationality.Name = txtName.Text.Trim();
            SelectedNationality.Code = txtCode.Text.Trim().ToUpper();
            SelectedNationality.Description = txtDescription.Text.Trim();
            SelectedNationality.UpdatedDate = DateTime.UtcNow;
            SelectedNationality.UpdatedBy = "CurrentUser";

            try
            {
                if (_nationalities.Any(x => x.Id != SelectedNationality.Id && 
                                           (x.Name.Equals(SelectedNationality.Name, StringComparison.OrdinalIgnoreCase) || 
                                            x.Code.Equals(SelectedNationality.Code, StringComparison.OrdinalIgnoreCase))))
                {
                    MessageBox.Show("A nationality with this name or code already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var index = Nationalities.IndexOf(SelectedNationality);
                if (index >= 0)
                {
                    Nationalities[index] = SelectedNationality;
                }
                lstNationalities.SelectedItem = SelectedNationality;
                MessageBox.Show("Nationality updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating nationality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNationality == null)
            {
                MessageBox.Show("Please select a nationality to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete '{SelectedNationality.Name}'?", 
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    SelectedNationality.UpdatedDate = DateTime.UtcNow;
                    SelectedNationality.UpdatedBy = "CurrentUser";

                    Nationalities.Remove(SelectedNationality);
                    ClearForm();
                    MessageBox.Show("Nationality deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting nationality: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtCode.Clear();
            txtDescription.Clear();
            SelectedNationality = null;
            lstNationalities.SelectedItem = null;
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        public static List<Nationality> GetNationalities()
        {
            return _nationalities.OrderBy(x => x.Name).ToList();
        }
    }
}
