using System.Security.Cryptography;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;

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

        private readonly IServiceContainer _service;
        public EditInsuranceCompany(string companyName)
        {
            InitializeComponent();
            this.DataContext=this;
            this.ProductsGrid.ItemsSource = this.Products;
            this.ContactsGrid.ItemsSource = this.Contacts;

            _service = new ServiceContainer(new HttpClientService());

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
            var response = await _service.InsuranceClassApiService.GetAllClassesAsync();
            InsuranceClasses = new ObservableCollection<GetInsuranceClassDTO>(response.Data);

        }
        
        private async void ComboClass_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var CombBox = sender as ComboBox;
            if (CombBox == null || CombBox.SelectedValue == null) return;

            var currentRow = CombBox.DataContext as LinkedProduct;
            if (currentRow == null) return;

            int selectedClassId = (int)CombBox.SelectedValue;

            var response = await _service.InsuranceLobApiService.GetLOBByClassIdAsync(selectedClassId);

            if (currentRow.avaiableLOBs != null) currentRow.avaiableLOBs.Clear();

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

        private async void FillProducts(int id)
        {
            var InsuranceProductDate = await _service.InsuranceProductService.GetInsuranceProductByInsuranceIdAsync(id);
            var newCollection = InsuranceProductDate.Data.Select(s => new LinkedProduct
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

            var InsuranceContactDate = await _service.InsuranceContactService.GetInsuranceContactByInsuranceIdAsync(id);
            var newCollection = InsuranceContactDate.Data.Select(s => new Contact
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
            var result = await _service.InsuranceCompanyService.GetInsuranceCompanyByNameAsync(companyName);

            _id = result.Data.Id;
            txtName.Text = result.Data.CompanyName;
            txtNameArabic.Text = result.Data.CompanyNameAr;
            txtVATNo.Text = result.Data.VATNo;
            txtTele.Text = result.Data.Tele;
            txtAbbreviation.Text = result.Data.Abbreviation;
            txtCRNo.Text = result.Data.CRNo;
            txtEmail.Text = result.Data.Email;

            txtUnifiedNo.Text = result.Data.UnifiedNo;

            txtBuildingNo.Text= result.Data.BuildingNo;
            txtAdditionalNo.Text = result.Data.AdditionalNo;
            txtBuildingName.Text = result.Data.BuildingName;
            txtBuildingNameArabic.Text = result.Data.BuildingNameArabic;
            txtStreetName.Text = result.Data.StreetName;
            txtStreetlNameArabic.Text = result.Data.StreetNameArabic;
            txtDistrictName.Text = result.Data.DistrictName;
            txtDistrictNameArabic.Text = result.Data.DistrictNameArabic;
            txtPostalCode.Text = result.Data.PostalZIPCode;
            txtCityName.Text = result.Data.CityName;
            txtCityNameArabic.Text = result.Data.CityNameArabic;
            txtState.Text = result.Data.State;
            txtStateArabic.Text = result.Data.StateArabic;
            txtCountry.Text = result.Data.CountryRegion;
            txtCountryArabic.Text = result.Data.CountryRegionArabic;

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
            _service.InsuranceCompanyService.UpdateInsuranceCompanyAsync(dto);
            Clear();

        }
    }
}
