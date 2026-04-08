using System.Collections.Specialized;

namespace InsuranceBrokerSystem.UI.Views.MasterTable
{
    /// <summary>
    /// Interaction logic for NewInsuranceCompany.xaml
    /// </summary>
    public partial class NewInsuranceCompany : Window
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<LinkedProduct> Products { get; set; } = new ObservableCollection<LinkedProduct>();
        public ObservableCollection<GetInsuranceClassDTO> InsuranceClasses { get; set; } = new ObservableCollection<GetInsuranceClassDTO>();
        public ObservableCollection<GetInsuranceLOBDTO> LinesOfBusiness { get; set; } = new ObservableCollection<GetInsuranceLOBDTO>();

        public ObservableCollection<string> DepartmentList { get; set; }

        private readonly InsuranceCompanyService _service;

        private readonly InsuranceClassApiService _ClassService;
        private readonly InsuranceLOBApiService _LOBService;
        public NewInsuranceCompany()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ProductsGrid.ItemsSource = this.Products;
            this.ContactsGrid.ItemsSource = this.Contacts;

            _service = new InsuranceCompanyService();
            _ClassService = new InsuranceClassApiService();
            _LOBService = new InsuranceLOBApiService();

            DepartmentList = new ObservableCollection<string>{"Underwriting","Customer Service","Claims","Finance","Administration","Management","Sales","Broker Relation"};
            FillClassesDate();
        }

        private void Clear()
        {
            this.Contacts.Clear();
            this.Products.Clear();
            this.InsuranceClasses.Clear();
            this.LinesOfBusiness.Clear();
            this.txtBuildingNo.Clear();
            this.txtBuildingName.Clear();
            this.txtStreetName.Clear();
            this.txtDistrictName.Clear();
            this.txtCityName.Clear();
            this.txtCountry.Clear();
            this.txtState.Clear();
            this.txtPostalCode.Clear();
            this.txtAdditionalNo.Clear();
            this.txtBuildingNameArabic.Clear();
            this.txtStreetlNameArabic.Clear();
            this.txtDistrictNameArabic.Clear();
            this.txtCityNameArabic.Clear();
            this.txtCountryArabic.Clear();
            this.txtStateArabic.Clear();
            this.txtName.Clear();
            this.txtNameArabic.Clear();
            this.txtAbbreviation.Clear();
            this.txtTele.Clear();
            this.txtVATNo.Clear();
            this.txtCRNo.Clear();
            this.txtUnifiedNo.Clear();
            this.txtEmail.Clear();
        }
        private async void FillClassesDate()
        {
            var response = await _ClassService.GetAllClassesAsync();
            InsuranceClasses = new ObservableCollection<GetInsuranceClassDTO>(response.Data);
            
        }
        private async void ComboClass_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var CombBox = sender as ComboBox;
            if(CombBox ==null || CombBox.SelectedValue == null) return;

            var currentRow = CombBox.DataContext as LinkedProduct;
            if(currentRow == null) return;

            int selectedClassId = (int)CombBox.SelectedValue;

            var response = await _LOBService.GetLOBByClassIdAsync(selectedClassId);

            if(currentRow.avaiableLOBs != null) currentRow.avaiableLOBs.Clear();
            
            currentRow.avaiableLOBs = new ObservableCollection<GetInsuranceLOBDTO>(response.Data);
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<AddInsuranceContractDTO> InsuranceContractDTO = new List<AddInsuranceContractDTO>();

            InsuranceContractDTO = Contacts.Select(i => new AddInsuranceContractDTO
            {
                ContactName = i.Name,
                ContactEmail = i.Email,
                ContactMobileNo = i.Mobile,
                Department = i.Department
            }).ToList();

            List<AddInsuranceProductDTO> InsuranceProducttDTO = new List<AddInsuranceProductDTO>();

            InsuranceProducttDTO = Products.Select(i => new AddInsuranceProductDTO
            {
                ClassId = i.ClassId,
                LineOfBusinessId = i.LobId
            }).ToList();

            AddInsuranceCompanyDTO dto = new AddInsuranceCompanyDTO
            {
                CompanyName = txtName.Text,
                CompanyNameAr = txtNameArabic.Text,
                VATNo = txtVATNo.Text,
                Tele = txtTele.Text,
                Abbreviation = txtAbbreviation.Text,
                CRNo = txtCRNo.Text,
                Email = txtEmail.Text,

                UnifiedNo = txtUnifiedNo.Text,

                BuildingNo = txtBuildingNo.Text,
                AdditionalNo = txtAdditionalNo.Text,
                BuildingName = txtBuildingName.Text,
                BuildingNameArabic = txtBuildingNameArabic.Text,
                StreetName = txtStreetName.Text,
                StreetNameArabic = txtStreetlNameArabic.Text,
                DistrictName = txtDistrictName.Text,
                DistrictNameArabic = txtDistrictNameArabic.Text,
                PostalZIPCode = txtPostalCode.Text,
                CityName = txtCityName.Text,
                CityNameArabic = txtCityNameArabic.Text,
                State = txtState.Text,
                StateArabic = txtStateArabic.Text,
                CountryRegion = txtCountry.Text,
                CountryRegionArabic = txtCountryArabic.Text,
                Products = InsuranceProducttDTO,
                Contacts = InsuranceContractDTO

            };
            _service.AddInsuranceCompanyAsync(dto);
            Clear();

        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var row = button.CommandParameter;
            if (row != null && Products != null)
            {
                Products.Remove((LinkedProduct)row);
            }
        }
        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var row = button.CommandParameter;
            if (row != null && Contacts != null)
            {
                Contacts.Remove((Contact)row);
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            Products.Add(new LinkedProduct());
        }
        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            Contacts.Add(new Contact());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Department { get; set; }
    }

    public class LinkedProduct:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int LobId { get; set; }

        private ObservableCollection<GetInsuranceLOBDTO> _avaiableLOBs { get; set; } = new();

        public ObservableCollection<GetInsuranceLOBDTO> avaiableLOBs 
        { get => _avaiableLOBs;
          set
            {
                _avaiableLOBs = value;
                OnPropertyChanged(nameof(avaiableLOBs));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)=> PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
