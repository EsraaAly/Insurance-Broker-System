
namespace InsuranceBrokerSystem.UI.Views.MasterTable
{
    /// <summary>
    /// Interaction logic for InsuranceClassesRegistry.xaml
    /// </summary>
    public partial class InsuranceClassesRegistry : UserControl
    {

        private readonly InsuranceClassApiService service;
        private readonly InsuranceLOBApiService LOBService;
        public ObservableCollection<GetInsuranceClassDTO> AllClassesList { get; set; } = new ObservableCollection<GetInsuranceClassDTO>();

        public InsuranceClassesRegistry()
        {
            InitializeComponent();
            service = new InsuranceClassApiService();
            LOBService = new InsuranceLOBApiService();
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

                var data = await service.AddClassAsync(newClass);
                ClearClass();
                LoadClassesDate();


            }
            catch (Exception ex)
            {
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
                await service.UpdateClassAsync(dto);
                LoadClassesDate();
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
                await service.DeleteClassAsync(Id);
                LoadClassesDate();
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
            bool success = await LOBService.AddLOBAsync(addInsuranceLOBDTO);
            if (success)
            {
                ClearLine();
                LoadLOBsDate();
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
                await LOBService.UpdateLOBAsync(dto);
               LoadLOBsDate();
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
                await LOBService.DeleteLOBAsync(Id);
                LoadLOBsDate();
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

                var data = await service.GetAllClassesAsync();

                // Bind the result to your DataGrid
                GridInsuranceClasses.ItemsSource = data;
                CombClass.ItemsSource = data;
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
                var dataList = await service.GetAllClassesAsync();
                AllClassesList = new ObservableCollection<GetInsuranceClassDTO>(dataList);

                // 2. Load the Lines of Business
                var data = await LOBService.GetAllLOBAsync();
        
                if (data != null)
                {
                    GridLines.ItemsSource = data;
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
