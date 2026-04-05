using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace InsuranceBrokerSystem.UI.Views.Clients
{
    /// <summary>
    /// Interaction logic for RegisterNewClient.xaml
    /// </summary>
    public partial class RegisterNewClient : Window
    {
        // Observable collections for the grids
        public ObservableCollection<ContactItem> Contacts { get; set; }
        public ObservableCollection<DocumentItem> Documents { get; set; }
        public ObservableCollection<BankAccountItem> BankAccounts { get; set; }

        public RegisterNewClient()
        {
            InitializeComponent();

            // Initialize collections
            Contacts = new ObservableCollection<ContactItem>();
            Documents = new ObservableCollection<DocumentItem>();
            BankAccounts = new ObservableCollection<BankAccountItem>();

            // Bind to DataGrids
            GridContactPersons.ItemsSource = Contacts;
            GridFiles.ItemsSource = Documents;
            GridBankAccounts.ItemsSource = BankAccounts;
        }

        // ====================== BUTTON EVENTS ======================

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add your save logic + validation here
            MessageBox.Show("Client has been saved successfully!",
                            "Success",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
            MessageBox.Show("All fields have been cleared.",
                            "Cleared",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        // ====================== ADD / DELETE EVENTS ======================

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            Contacts.Add(new ContactItem
            {
                Name = "New Contact Person",
                Position = "",
                Extension = "",
                Mobile = "",
                Tele = "",
                Email = ""
            });
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is ContactItem item)
            {
                Contacts.Remove(item);
            }
        }

        private void btnBrowseDocument_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select Documents to Upload",
                Multiselect = true,
                Filter = "All Supported Files|*.pdf;*.docx;*.doc;*.xlsx;*.xls;*.png;*.jpg;*.jpeg;*.txt|" +
                         "PDF Files|*.pdf|" +
                         "Word Documents|*.docx;*.doc|" +
                         "Excel Files|*.xlsx;*.xls|" +
                         "Images|*.png;*.jpg;*.jpeg|" +
                         "All Files|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                foreach (var filePath in dialog.FileNames)
                {
                    AddDocumentToGrid(filePath);
                }
            }
        }

        private void AddDocumentToGrid(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

            // Avoid duplicates
            if (Documents.Any(d => d.FilePath == filePath)) return;

            var info = new FileInfo(filePath);
            Documents.Add(new DocumentItem
            {
                Index        = Documents.Count + 1,
                FileName     = info.Name,
                FilePath     = filePath,
                DocumentType = GetDocumentType(info.Extension),
                FileSize     = FormatFileSize(info.Length),
                UploadDate   = DateTime.Now.ToString("dd/MM/yyyy")
            });
        }

        private string GetDocumentType(string extension)
        {
            return extension.ToLower() switch
            {
                ".pdf"             => "PDF",
                ".docx" or ".doc"  => "Word Document",
                ".xlsx" or ".xls"  => "Excel Sheet",
                ".png" or ".jpg" or ".jpeg" => "Image",
                ".txt"             => "Text File",
                _                  => "Other"
            };
        }

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)       return $"{bytes} B";
            if (bytes < 1048576)    return $"{bytes / 1024.0:F1} KB";
            return                         $"{bytes / 1048576.0:F1} MB";
        }


        private void btnAddBank_Click(object sender, RoutedEventArgs e)
        {
            BankAccounts.Add(new BankAccountItem
            {
                BankName = "New Bank",
                Branch = "",
                IBAN = "",
                SwiftCode = ""
            });
        }

        private void DeleteBank_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is BankAccountItem item)
            {
                BankAccounts.Remove(item);
            }
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DocumentItem item)
            {
                Documents.Remove(item);
                // Re-index
                for (int i = 0; i < Documents.Count; i++)
                    Documents[i].Index = i + 1;
            }
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is DocumentItem item
                && !string.IsNullOrEmpty(item.FilePath) && File.Exists(item.FilePath))
            {
                Process.Start(new ProcessStartInfo(item.FilePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("File not found. It may have been moved or deleted.",
                                "File Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // ====================== DRAG & DROP SUPPORT ======================

        private void DropZone_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
            e.Handled = true;

            // Highlight the grid to signal it accepts the drop
            GridFiles.Background = new SolidColorBrush(Color.FromRgb(209, 250, 229));
        }

        private void DropZone_DragLeave(object sender, DragEventArgs e)
        {
            GridFiles.Background = new SolidColorBrush(Colors.White); // reset
        }

        private void DropZone_Drop(object sender, DragEventArgs e)
        {
            GridFiles.Background = new SolidColorBrush(Colors.White); // reset

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                    AddDocumentToGrid(file);
            }
        }


        // ====================== HELPER METHOD ======================

        private void ClearAllFields()
        {
            // Clear TextBoxes
            txtClientName.Clear();
            txtClientNameAr.Clear();
            txtOfficialName.Clear();
            txtIDNo.Clear();
            txtCommercialNo.Clear();
            txtTele.Clear();
            txtEmail.Clear();
            txtBuildingNo.Clear();
            txtDistrict.Clear();
            txtProspPoBox.Clear();

            // Reset ComboBoxes
            ResetComboBox(CombPolicyType);
            ResetComboBox(ComboRelationshipStatus);
            ResetComboBox(CombBusinessType);
            ResetComboBox(CmbProspectType);
            ResetComboBox(ComboRegistrationStatus);
            ResetComboBox(cmbBusinessActivity);
            ResetComboBox(cmbMarketSegmant);
            ResetComboBox(CombLocation);
            ResetComboBox(CombProducers);
            ResetComboBox(CombScreeningResult);

            // Clear DatePickers
            DTPIncorporation.SelectedDate = null;
            DTPIDExpiryDate.SelectedDate = null;
            txtBirthDay.SelectedDate = null;

            // Clear Grids
            Contacts.Clear();
            Documents.Clear();
            BankAccounts.Clear();
        }

        private void ResetComboBox(ComboBox comboBox)
        {
            if (comboBox != null && comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }
    }

    // ====================== SUPPORTING MODEL CLASSES ======================

    public class ContactItem
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Extension { get; set; }
        public string Mobile { get; set; }
        public string Tele { get; set; }
        public string Email { get; set; }
    }

    public class DocumentItem
    {
        public int    Index        { get; set; }
        public string FileName     { get; set; } = string.Empty;
        public string FilePath     { get; set; } = string.Empty;
        public string DocumentType { get; set; } = "Other";
        public string FileSize     { get; set; } = string.Empty;
        public string UploadDate   { get; set; } = string.Empty;
    }

    public class BankAccountItem
    {
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }
    }
}
