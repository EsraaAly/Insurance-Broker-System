using System.Security.Cryptography;

namespace InsuranceBrokerSystem.UI.Views.MasterTable
{
    /// <summary>
    /// Interaction logic for EditInsuranceCompany.xaml
    /// </summary>
    public partial class EditInsuranceCompany : Window
    {
        private int _id;
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public ObservableCollection<LinkedProduct> Products { get; set; } = new ObservableCollection<LinkedProduct>();
        public ObservableCollection<GetInsuranceClassDTO> InsuranceClasses { get; set; } = new ObservableCollection<GetInsuranceClassDTO>();
        public ObservableCollection<GetInsuranceLOBDTO> LinesOfBusiness { get; set; } = new ObservableCollection<GetInsuranceLOBDTO>();

        public ObservableCollection<string> DepartmentList { get; set; }

        private readonly InsuranceCompanyService _InsuranceCompanyService;
        private readonly InsuranceContactService _InsuranceContactService;
        private readonly InsuranceProductService _InsuranceProductService;

        private readonly InsuranceClassApiService _ClassService;
        private readonly InsuranceLOBApiService _LOBService;
        public EditInsuranceCompany(string companyName)
        {
            InitializeComponent();
            this.DataContext=this;
            this.ProductsGrid.ItemsSource = this.Products;
            this.ContactsGrid.ItemsSource = this.Contacts;

            _InsuranceCompanyService = new InsuranceCompanyService();
            _InsuranceContactService = new InsuranceContactService();
            _InsuranceProductService = new InsuranceProductService();
            _ClassService = new InsuranceClassApiService();
            _LOBService = new InsuranceLOBApiService();

            DepartmentList = new ObservableCollection<string> { "Underwriting", "Customer Service", "Claims", "Finance", "Administration", "Management", "Sales", "Broker Relation" };
            FillClassesDate();
            FillInsurance(companyName);
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
            var ClassesDate = await _ClassService.GetAllClassesAsync();
            InsuranceClasses = new ObservableCollection<GetInsuranceClassDTO>(ClassesDate);

        }
        
        private async void ComboClass_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var CombBox = sender as ComboBox;
            if (CombBox == null || CombBox.SelectedValue == null) return;

            var currentRow = CombBox.DataContext as LinkedProduct;
            if (currentRow == null) return;

            int selectedClassId = (int)CombBox.SelectedValue;

            var LOBsDate = await _LOBService.GetLOBByClassIdAsync(selectedClassId);

            if (currentRow.avaiableLOBs != null) currentRow.avaiableLOBs.Clear();

            currentRow.avaiableLOBs = new ObservableCollection<GetInsuranceLOBDTO>(LOBsDate);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private async void FillProducts(int id)
        {
            var InsuranceProductDate = await _InsuranceProductService.GetInsuranceProductByInsuranceIdAsync(id);
            var newCollection = InsuranceProductDate.Select(s => new LinkedProduct
            {
                Id = s.Id,
                ClassId = s.ClassId,
                LobId = s.LineOfBusinessId,
            });
            Products = new ObservableCollection<LinkedProduct>(newCollection);
            this.ProductsGrid.ItemsSource = this.Products;

        }

        private async void FillContacts(int id)
        {

            var InsuranceContactDate = await _InsuranceContactService.GetInsuranceContactByInsuranceIdAsync(id);
            var newCollection = InsuranceContactDate.Select(s => new Contact
            {
                Id = s.Id,
                Department = s.Department,
                Email = s.ContactEmail,
                Mobile = s.ContactMobileNo,
                Name = s.ContactName,
            });
            Contacts = new ObservableCollection<Contact>(newCollection);
            this.ContactsGrid.ItemsSource = this.Contacts;

        }

        private async void FillInsurance(string companyName)
        {
            var InsuranceDate = await _InsuranceCompanyService.GetInsuranceCompanyByNameAsync(companyName);

            _id = InsuranceDate.Id;
            txtName.Text = InsuranceDate.CompanyName;
            txtNameArabic.Text = InsuranceDate.CompanyNameAr;
            txtVATNo.Text = InsuranceDate.VATNo;
            txtTele.Text = InsuranceDate.Tele;
            txtAbbreviation.Text = InsuranceDate.Abbreviation;
            txtCRNo.Text = InsuranceDate.CRNo;
            txtEmail.Text = InsuranceDate.Email;

            txtUnifiedNo.Text = InsuranceDate.UnifiedNo;

            txtBuildingNo.Text= InsuranceDate.BuildingNo;
            txtAdditionalNo.Text = InsuranceDate.AdditionalNo;
            txtBuildingName.Text = InsuranceDate.BuildingName;
            txtBuildingNameArabic.Text = InsuranceDate.BuildingNameArabic;
            txtStreetName.Text = InsuranceDate.StreetName;
            txtStreetlNameArabic.Text = InsuranceDate.StreetNameArabic;
            txtDistrictName.Text = InsuranceDate.DistrictName;
            txtDistrictNameArabic.Text =InsuranceDate.DistrictNameArabic;
            txtPostalCode.Text = InsuranceDate.PostalZIPCode;
            txtCityName.Text = InsuranceDate.CityName;
            txtCityNameArabic.Text = InsuranceDate.CityNameArabic;
            txtState.Text = InsuranceDate.State;
            txtStateArabic.Text = InsuranceDate.StateArabic;
            txtCountry.Text = InsuranceDate.CountryRegion;
            txtCountryArabic.Text = InsuranceDate.CountryRegionArabic;

            FillProducts(_id);
            FillContacts(_id);
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<UpdateInsuranceContractDTO> InsuranceContractDTO = new List<UpdateInsuranceContractDTO>();

            InsuranceContractDTO = Contacts.Select(i => new UpdateInsuranceContractDTO
            {
                Id = i.Id,
                ContactName = i.Name,
                ContactEmail = i.Email,
                ContactMobileNo = i.Mobile,
                Department = i.Department
            }).ToList();

            List<UpdateInsuranceProductDTO> InsuranceProducttDTO = new List<UpdateInsuranceProductDTO>();

            InsuranceProducttDTO = Products.Select(i => new UpdateInsuranceProductDTO
            {
                Id = i.Id,
                ClassId = i.ClassId,
                LineOfBusinessId = i.LobId
            }).ToList();

            UpdateInsuranceCompanyDTO dto = new UpdateInsuranceCompanyDTO
            {
                Id=_id,
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
            _InsuranceCompanyService.UpdateInsuranceCompanyAsync(dto);
            Clear();

        }
    }
}
