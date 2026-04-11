using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;

namespace InsuranceBrokerSystem.UI.Views.MasterTable
{
    /// <summary>
    /// Interaction logic for InsuranceClassesRegistry.xaml
    /// </summary>
    public partial class InsuranceClassesRegistry : UserControl
    {

        private readonly IServiceContainer _service;
        public ObservableCollection<GetInsuranceClassDTO> AllClassesList { get; set; } = new ObservableCollection<GetInsuranceClassDTO>();

        public InsuranceClassesRegistry()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            this.DataContext = this;
            LoadClassesDate();
            LoadLOBsDate();
        }

        // --- Tab 1: Insurance Classes ---

        private async void SaveClass_Click(object sender, RoutedEventArgs e)
        {
            var newClass = new AddInsuranceClassDTO
            {
                ClassName = txtInsuranceClass.Text,
                Abbreviation = txtAbbreviation.Text
            };

            try
            {
                var result = await _service.InsuranceClassApiService.AddClassAsync(newClass);
                if (result.Successed)
                {
                    ClearClass();
                    LoadClassesDate();
                }
                else
                {
                    ApiResponseHandler.ShowError($"Error saving class: {result.Message}");
                }
            }
            catch (Exception ex)
            {
                ApiResponseHandler.ShowError($"Error saving class: {ex.Message}");
            }
        }

        private void ClearClass_Click(object sender, RoutedEventArgs e)
        {
            ClearClass();
        }
        private void ClearClass()
        {
            txtInsuranceClass.Clear();
            txtAbbreviation.Clear();
        }


        private async void UpdateClass_Click(object sender, RoutedEventArgs e)
        {
           // 1.Identify the button that was clicked
            var button = sender as Button;
            UpdateInsuranceClassDTO dto;
            // 2. Extract the data object from the CommandParameter
            var selectedRowData = button.CommandParameter as GetInsuranceClassDTO;

            if (selectedRowData != null)
            {
                 dto = new UpdateInsuranceClassDTO
                {
                     Id = selectedRowData.Id,
                    ClassName = selectedRowData.ClassName,
                    Abbreviation = selectedRowData.Abbreviation
                };
                var result = await _service.InsuranceClassApiService.UpdateClassAsync(dto);
                if (result.Successed)
                {
                    LoadClassesDate();
                }
                else
                {
                    ApiResponseHandler.ShowError($"Error updating class: {result.Message}");
                }
            }
                      
        }

        private async void DeleteClass_Click(object sender, RoutedEventArgs e)
        {
            // 1.Identify the button that was clicked
            var button = sender as Button;
            int Id = 0;
            // 2. Extract the data object from the CommandParameter
            var selectedRowData = button.CommandParameter as GetInsuranceClassDTO;

            if (selectedRowData != null)
            {
                Id = selectedRowData.Id;
                var result = await _service.InsuranceClassApiService.DeleteClassAsync(Id);
                if (result.Successed)
                {
                    LoadClassesDate();
                }
                else
                {
                    ApiResponseHandler.ShowError($"Error deleting class: {result.Message}");
                }
            }
        }

        // --- Tab 2: Line of Businesses ---

        private async void SaveLine_Click(object sender, RoutedEventArgs e)
        {
            AddInsuranceLOBDTO addInsuranceLOBDTO = new AddInsuranceLOBDTO
            {
                ClassID = (CombClass.SelectedItem as GetInsuranceClassDTO)?.Id ?? 0,
                LineOfBusiness = txtLineOfBusiness.Text,
                Abbreviation = txtAbb.Text,
            };
            var result = await _service.InsuranceLobApiService.AddLOBAsync(addInsuranceLOBDTO);
            if (result.Successed)
            {
                ClearLine();
                LoadLOBsDate();
            }
            else
            {
                ApiResponseHandler.ShowError($"Error saving line of business: {result.Message}");
            }
        }

        private void ClearLine_Click(object sender, RoutedEventArgs e)
        {
            ClearLine();
        }
        private void ClearLine()
        {
            txtLineOfBusiness.Clear();
            txtAbb.Clear();
            CombClass.SelectedIndex = -1;
        }

        private async void UpdateLine_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            UpdateInsuranceLOBDTO dto;
            // 2. Extract the data object from the CommandParameter
            var selectedRowData = button.CommandParameter as GetInsuranceLOBDTO;
            if (selectedRowData != null)
            {
                dto = new UpdateInsuranceLOBDTO
                {
                    Id = selectedRowData.Id,
                    LineOfBusiness = selectedRowData.LineOfBusiness,
                    Abbreviation = selectedRowData.Abbreviation,
                    ClassID = selectedRowData.ClassID,
                };
                var result = await _service.InsuranceLobApiService.UpdateLOBAsync(dto);
                if (result.Successed)
                {
                    LoadLOBsDate();
                }
                else
                {
                    ApiResponseHandler.ShowError($"Error updating line of business: {result.Message}");
                }
            }
        }

        private async void DeleteLine_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int Id = 0;
            // 2. Extract the data object from the CommandParameter
            var selectedRowData = button.CommandParameter as GetInsuranceLOBDTO;
            if (selectedRowData != null)
            {
                Id = selectedRowData.Id;
                var result = await _service.InsuranceLobApiService.DeleteLOBAsync(Id);
                if (result.Successed)
                {
                    LoadLOBsDate();
                }
                else
                {
                    ApiResponseHandler.ShowError($"Error deleting line of business: {result.Message}");
                }
            }
        }

        private async void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadClassesDate();
        }
        private async void LoadClassesDate()
        {
            try
            {

                var response = await _service.InsuranceClassApiService.GetAllClassesAsync();
                if (response.Successed)
                {
                    var data = response.Data;
                    // Bind the result to your DataGrid
                    GridInsuranceClasses.ItemsSource = data;
                    CombClass.ItemsSource = data;
                }
                else
                {
                    ApiResponseHandler.ShowError($"Error loading classes: {response.Message}");
                }
                // This makes sure the first item is automatically selected!
                if (CombClass.Items.Count > 0)
                {
                    CombClass.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
            }
            finally
            {

            }
        }

        protected 
        private async void LoadLOBsDate()
        {
            try
            {
                // 1. First, make sure the Master Classes are loaded
                var classResponse = await _service.InsuranceClassApiService.GetAllClassesAsync();
                if (classResponse.Successed)
                {
                    AllClassesList = new ObservableCollection<GetInsuranceClassDTO>(classResponse.Data);
                }

                // 2. Load the Lines of Business
                var lobResponse = await _service.InsuranceLobApiService.GetAllLOBAsync();
                if (lobResponse.Successed)
                {
                    GridLines.ItemsSource = lobResponse.Data;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Could not connect to the Insurance API: {ex.Message}");
            }
            finally
            {

            }
        }

        public class ClassItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
