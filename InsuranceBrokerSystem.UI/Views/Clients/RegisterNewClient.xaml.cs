

using InsuranceBrokerSystem.UI.Services;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.PolicyType;
using GetPolicyTypeDTO = InsuranceBrokerSystem.Application.DTOs.Master_Table.PolicyType.GetPolicyTypeDTO;

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

        public ObservableCollection<GetPolicyTypeDTO> PolicyTypes  = new ObservableCollection<GetPolicyTypeDTO>();
        public ObservableCollection<GetNationalityDTO> Nationalities = new ObservableCollection<GetNationalityDTO>();
        public ObservableCollection<GetBusinessActivityDTO> BusinessActivities = new ObservableCollection<GetBusinessActivityDTO>();
        public ObservableCollection<GetLocationDTO> Locations = new ObservableCollection<GetLocationDTO>();
        public ObservableCollection<GetSourceOfIncomeDTO> SourceOfIncomes = new ObservableCollection<GetSourceOfIncomeDTO>();


        private readonly IServiceContainer _service;

        private readonly GetClientDTO _editingClient;

        public RegisterNewClient()
        {
            InitializeComponent();
            DataContext = this;
            _service = new ServiceContainer(new HttpClientService());
            InitializeWindow();
        }

        public RegisterNewClient(GetClientDTO clientToEdit) : this()
        {
            _editingClient = clientToEdit;
            LoadClientData();
            Title = "Edit Client";
        }
        private void InitializeWindow()
        {
            // Initialize collections
            Contacts = new ObservableCollection<ContactItem>();
            Documents = new ObservableCollection<DocumentItem>();
            BankAccounts = new ObservableCollection<BankAccountItem>();

            // Populate comboboxes
            PopulateComboBoxes();

            // Bind to DataGrids
            GridContactPersons.ItemsSource = Contacts;
            GridFiles.ItemsSource = Documents;
            GridBankAccounts.ItemsSource = BankAccounts;
        }
        private async void PopulateComboBoxes()
        {
            // Master Data Comboboxes
            await PopulateMasterDataComboBoxes();
            
            // Other comboboxes (keeping existing hardcoded values for now)
            PopulateOtherComboBoxes();
        }

        private async Task PopulateMasterDataComboBoxes()
        {
            try
            {
                FillPolicyTypeFallback();

                FillNationalityFallback();

                FillSourceOfIncomeFallback();

                FillBusinessActivityFallback();

                FillLocationFallback();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from database: {ex.Message}", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void FillPolicyTypeFallback()
        {
            PolicyTypes.Clear();
            var response = await _service.PolicyTypeApiService.GetAllPolicyTypesAsync();

            if (response.Successed && response.Data != null)
            {
                PolicyTypes.Clear();

                foreach (var item in response.Data)
                {
                    PolicyTypes.Add(item);
                }
                CombPolicyType.ItemsSource = PolicyTypes;
            }
        }

        private async void FillNationalityFallback()
        {
            Nationalities.Clear();
            var nationalitiesResponse = await _service.NationalityApiService.GetAllNationalitiesAsync();
            if (nationalitiesResponse.Successed && nationalitiesResponse.Data != null)
            {
                foreach (var nationality in nationalitiesResponse.Data)
                {
                    Nationalities.Add(nationality);
                }
                CombNationality.ItemsSource = Nationalities;
            }
        }

        private async void FillSourceOfIncomeFallback()
        {
            SourceOfIncomes.Clear();
            var sourceOfIncomesResponse = await _service.SourceOfIncomeApiService.GetAllSourceOfIncomesAsync();
            if (sourceOfIncomesResponse.Successed && sourceOfIncomesResponse.Data != null)
            {
                foreach (var source in sourceOfIncomesResponse.Data)
                {
                    SourceOfIncomes.Add(source);
                }
                CombSourceofIncome.ItemsSource = SourceOfIncomes;
            }
        }

        private async void FillBusinessActivityFallback()
        {
            BusinessActivities.Clear();
            var businessActivitiesResponse = await _service.BusinessActivityApiService.GetAllBusinessActivitiesAsync();
            if (businessActivitiesResponse.Successed && businessActivitiesResponse.Data != null)
            {
                foreach (var activity in businessActivitiesResponse.Data)
                {
                    BusinessActivities.Add(activity);
                }
                cmbBusinessActivity.ItemsSource = BusinessActivities;
            }
        }

        private async void FillLocationFallback()
        {
            Locations.Clear();
            var locationsResponse = await _service.LocationApiService.GetAllLocationsAsync();
            if (locationsResponse.Successed && locationsResponse.Data != null)
            {
                foreach (var location in locationsResponse.Data)
                {
                    Locations.Add(location);
                }
                CombLocation.ItemsSource = Locations;
            }
        }

        private void PopulateOtherComboBoxes()
        {
            // Registration Status
            ComboCorporateRegistrationStatus.Items.Clear();
            ComboCorporateRegistrationStatus.Items.Add("Joint Liability Company");
            ComboCorporateRegistrationStatus.Items.Add("Limited Partnership Company");
            ComboCorporateRegistrationStatus.Items.Add("Joint Venture");
            ComboCorporateRegistrationStatus.Items.Add("Joint Stock");
            ComboCorporateRegistrationStatus.Items.Add("Limited Liability Company");

            ComboRelationshipStatus.Items.Clear();
            ComboRelationshipStatus.Items.Add("Joint Liability Company");
            ComboRelationshipStatus.Items.Add("Limited Partnership Company");
            ComboRelationshipStatus.Items.Add("Joint Venture");
            ComboRelationshipStatus.Items.Add("Joint Stock");
            ComboRelationshipStatus.Items.Add("Limited Liability Company");
            // Market Segment
            cmbMarketSegmant.Items.Clear();
            cmbMarketSegmant.Items.Add("Local");
            cmbMarketSegmant.Items.Add("Regional");
            cmbMarketSegmant.Items.Add("Multi-National (local)");
            cmbMarketSegmant.Items.Add("Multi-National (Regional)");

            // Premium
            CombPremium.Items.Clear();
            CombPremium.Items.Add("<5,000 SAR");
            CombPremium.Items.Add("5,000 SAR - 10,000 SAR");
            CombPremium.Items.Add("10,000 SAR - 50,000 SAR");
            CombPremium.Items.Add("50,000 SAR - 100,000 SAR");
            CombPremium.Items.Add("100,000 SAR - 500,000 SAR");
            CombPremium.Items.Add(">500,000 SAR");

            // Channel
            CombChannel.Items.Clear();
            CombChannel.Items.Add("Business Development");
            CombChannel.Items.Add("Direct");
            CombChannel.Items.Add("Referral");

            // Interface
            CombInterface.Items.Clear();
            CombInterface.Items.Add("Face-to-Face");
            CombInterface.Items.Add("Conducted via phone");
            CombInterface.Items.Add("Conducted via email");
            CombInterface.Items.Add("Indirect Parties");

            // Producer 1 and 2
            CombProducers.Items.Clear();
            CombProducers2nd.Items.Clear();
            var producers = new List<string> { "Jeddah", "Riyadh", "Khobar" };
            foreach (var producer in producers)
            {
                CombProducers.Items.Add(producer);
                CombProducers2nd.Items.Add(producer);
            }

            // Screening Result
            CombScreeningResult.Items.Clear();
            CombScreeningResult.Items.Add("Other");
            CombScreeningResult.Items.Add("Sanctioned");
            CombScreeningResult.Items.Add("Politically Exposed Person(s)");
            CombScreeningResult.Items.Add("No Match");

            // Client Type
            CmbProspectType.Items.Clear();
            CmbProspectType.Items.Add(new ComboBoxItem { Content = "Retail", Tag = (int)ClientType.Retail });
            CmbProspectType.Items.Add(new ComboBoxItem { Content = "Corporate", Tag = (int)ClientType.Corporate });

            // Business Type
            CombBusinessType.Items.Clear();
            CombBusinessType.Items.Add("Governmental Entities");
            CombBusinessType.Items.Add("Semi-Government");
            CombBusinessType.Items.Add("Publicly listed");
            CombBusinessType.Items.Add("Corporation");
            CombBusinessType.Items.Add("SME");
            CombBusinessType.Items.Add("Individual");
        }

        private void CmbProspectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbProspectType.SelectedItem is ComboBoxItem selectedItem)
            {
                int clientType = (int)selectedItem.Tag;
                ToggleFieldsByClientType(clientType);
            }
        }

        private void ToggleFieldsByClientType(int clientType)
        {
            bool isRetail = clientType == (int)ClientType.Retail;
            bool isCorporate = clientType == (int)ClientType.Corporate;

            // Retail fields - enabled only for Retail clients
            txtIDNo.IsEnabled = isRetail;
            DTPIDExpiryDate.IsEnabled = isRetail;
            txtIdExpirydateHijir.IsEnabled = isRetail;
            CombNationality.IsEnabled = isRetail;
            CombSourceofIncome.IsEnabled = isRetail;
            txtEmail.IsEnabled = isRetail;
            txtBirthDay.IsEnabled = isRetail;

            // Corporate fields - enabled only for Corporate clients
            ComboCorporateRegistrationStatus.IsEnabled = isCorporate;
            cmbBusinessActivity.IsEnabled = isCorporate;
            cmbMarketSegmant.IsEnabled = isCorporate;
            DTPIncorporation.IsEnabled = isCorporate;
            txtDateofIncorporationHijri.IsEnabled = isCorporate;
            txtCommercialNo.IsEnabled = isCorporate;
            DTPExpiryDate.IsEnabled = isCorporate;
            txtExpiryHijri.IsEnabled = isCorporate;
            txtSponsorID.IsEnabled = isCorporate;
            txtUnifiedNo.IsEnabled = isCorporate;
            txtVatNo.IsEnabled = isCorporate;
            txtCapital.IsEnabled = isCorporate;
            CombPremium.IsEnabled = isCorporate;

            // Clear fields when disabled
            if (!isRetail)
            {
                txtIDNo.Clear();
                DTPIDExpiryDate.SelectedDate = null;
                txtIdExpirydateHijir.Clear();
                CombNationality.SelectedIndex = -1;
                CombSourceofIncome.SelectedIndex = -1;
                txtEmail.Clear();
                txtBirthDay.SelectedDate = null;
            }

            if (!isCorporate)
            {
                ComboCorporateRegistrationStatus.SelectedIndex = -1;
                cmbBusinessActivity.SelectedIndex = -1;
                cmbMarketSegmant.SelectedIndex = -1;
                DTPIncorporation.SelectedDate = null;
                txtDateofIncorporationHijri.Clear();
                txtCommercialNo.Clear();
                DTPExpiryDate.SelectedDate = null;
                txtExpiryHijri.Clear();
                txtSponsorID.Clear();
                txtUnifiedNo.Clear();
                txtVatNo.Clear();
                txtCapital.Clear();
                CombPremium.SelectedIndex = -1;
            }
        }

        // ====================== BUTTON EVENTS ======================

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateForm())
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_editingClient != null)
                {
                    // Update existing client
                    var updateDto = CreateUpdateClientDTO();
                    var result = await _service.ClientService.UpdateClientAsync(updateDto);
                    if (result != null)
                    {
                        DialogResult = true;
                        Close();
                    }
                }
                else
                {
                    // Add new client
                    var addDto = CreateAddClientDTO();
                    var result = await _service.ClientService.AddClientAsync(addDto);
                    if (result != null)
                    {
                        DialogResult = true;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadClientData()
        {
            if (_editingClient == null) return;

            // Load basic information
            txtClientName.Text = _editingClient.ClientName ?? string.Empty;
            txtClientNameAr.Text = _editingClient.ClientNameAr ?? string.Empty;
            txtOfficialName.Text = _editingClient.OfficialName ?? string.Empty;
            txtIDNo.Text = _editingClient.IdentityNo ?? string.Empty;
            txtCommercialNo.Text = _editingClient.CommercialRegistrationNo ?? string.Empty;
            txtTele.Text = _editingClient.Tele ?? string.Empty;
            txtEmail.Text = _editingClient.Email ?? string.Empty;
            txtBuildingNo.Text = _editingClient.BuildingNo ?? string.Empty;
            txtDistrict.Text = _editingClient.District ?? string.Empty;
            txtProspPoBox.Text = _editingClient.POBox ?? string.Empty;

            // Load dropdowns
            if (_editingClient.PolicyTypeId.HasValue)
                CombPolicyType.SelectedValue = _editingClient.PolicyTypeId.Value;
            
            CmbProspectType.SelectedValue = _editingClient.ClientType;
            ComboRelationshipStatus.SelectedValue = _editingClient.RelationshipStatus;
            ComboCorporateRegistrationStatus.SelectedValue = _editingClient.RegistrationStatusid;
            // Load dates
            DTPIncorporation.SelectedDate = _editingClient.DateOfIncorporation;
            DTPIDExpiryDate.SelectedDate = _editingClient.IDExpiryDate;
            txtBirthDay.SelectedDate = _editingClient.DateOfBirth;

            // Load contacts
            Contacts.Clear();
            foreach (var contact in _editingClient.Contacts)
            {
                Contacts.Add(new ContactItem
                {
                    Name = contact.Name ?? string.Empty,
                    Position = contact.Position ?? string.Empty,
                    Extension = contact.Extension ?? string.Empty,
                    Mobile = contact.Mobile ?? string.Empty,
                    Tele = contact.Tele ?? string.Empty,
                    Email = contact.Email ?? string.Empty
                });
            }

            // Load bank accounts
            BankAccounts.Clear();
            foreach (var bankAccount in _editingClient.BankAccounts)
            {
                BankAccounts.Add(new BankAccountItem
                {
                    BankName = bankAccount.BankName ?? string.Empty,
                    Branch = bankAccount.Branch ?? string.Empty,
                    IBAN = bankAccount.IBAN ?? string.Empty,
                    SwiftCode = bankAccount.SwiftCode ?? string.Empty
                });
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtClientName.Text))
                return false;

            if (CmbProspectType.SelectedItem == null)
                return false;
            var selectedPolicy = (GetPolicyTypeDTO)CmbProspectType.SelectedItem;
            var clientType = selectedPolicy.Id;
            //var clientType = int.Parse(CmbProspectType.SelectedItem.ToString());
            if (clientType == 1 && string.IsNullOrWhiteSpace(txtIDNo.Text))
                return false;

            if (clientType == 2 && string.IsNullOrWhiteSpace(txtCommercialNo.Text))
                return false;

            return true;
        }

        private AddClientDTO CreateAddClientDTO()
        {
            return new AddClientDTO
            {
                ClientName = txtClientName.Text,
                ClientNameAr = txtClientNameAr.Text,
                OfficialName = txtOfficialName.Text,
                ClientType = int.Parse(CmbProspectType.SelectedItem.ToString()),
                RelationshipStatus = ComboRelationshipStatus.SelectedIndex,
                IdentityNo = txtIDNo.Text,
                CommercialRegistrationNo = txtCommercialNo.Text,
                RegistrationStatusid = ComboCorporateRegistrationStatus.SelectedIndex,
                Email = txtEmail.Text,
                Tele = txtTele.Text,
                BuildingNo = txtBuildingNo.Text,
                District = txtDistrict.Text,
                POBox = txtProspPoBox.Text,
                DateOfBirth = txtBirthDay.SelectedDate,
                IDExpiryDate = DTPIDExpiryDate.SelectedDate,
                DateOfIncorporation = DTPIncorporation.SelectedDate,
                Contacts = Contacts.Select(c => new AddClientContactDTO
                {
                    Name = c.Name,
                    Position = c.Position,
                    Extension = c.Extension,
                    Mobile = c.Mobile,
                    Tele = c.Tele,
                    Email = c.Email
                }).ToList(),
                BankAccounts = BankAccounts.Select(b => new AddClientBankAccountDTO
                {
                    BankName = b.BankName,
                    Branch = b.Branch,
                    IBAN = b.IBAN,
                    SwiftCode = b.SwiftCode
                }).ToList(),
                Documents = Documents.Select(d => new AddClientDocumentDTO
                {
                    FileName = d.FileName,
                    FilePath = d.FilePath,
                    DocumentType = d.DocumentType,
                    FileSize = d.FileSize,
                    UploadDate = DateTime.Parse(d.UploadDate)
                }).ToList()
            };
        }

        private UpdateClientDTO CreateUpdateClientDTO()
        {
            return new UpdateClientDTO
            {
                Id = _editingClient.Id,
                ClientName = txtClientName.Text,
                ClientNameAr = txtClientNameAr.Text,
                OfficialName = txtOfficialName.Text,
                ClientType = int.Parse(CmbProspectType.SelectedItem.ToString()),
                RelationshipStatus = ComboRelationshipStatus.SelectedIndex,
                RegistrationStatusid = ComboCorporateRegistrationStatus.SelectedIndex,
                IdentityNo = txtIDNo.Text,
                CommercialRegistrationNo = txtCommercialNo.Text,
                Email = txtEmail.Text,
                Tele = txtTele.Text,
                BuildingNo = txtBuildingNo.Text,
                District = txtDistrict.Text,
                POBox = txtProspPoBox.Text,
                DateOfBirth = txtBirthDay.SelectedDate,
                IDExpiryDate = DTPIDExpiryDate.SelectedDate,
                DateOfIncorporation = DTPIncorporation.SelectedDate,
                PolicyTypeId = CombPolicyType.SelectedValue != null ? (int)CombPolicyType.SelectedValue : null,
                Contacts = Contacts.Select(c => new UpdateClientContactDTO
                {
                    Name = c.Name,
                    Position = c.Position,
                    Extension = c.Extension,
                    Mobile = c.Mobile,
                    Tele = c.Tele,
                    Email = c.Email
                }).ToList(),
                BankAccounts = BankAccounts.Select(b => new UpdateClientBankAccountDTO
                {
                    BankName = b.BankName,
                    Branch = b.Branch,
                    IBAN = b.IBAN
                }).ToList(),
                Documents = Documents.Select(d => new UpdateClientDocumentDTO
                {
                    FileName = d.FileName,
                    FilePath = d.FilePath,
                    DocumentType = d.DocumentType,
                    FileSize = d.FileSize,
                    UploadDate = DateTime.Parse(d.UploadDate)
                }).ToList()
            };
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
            ResetComboBox(ComboCorporateRegistrationStatus);
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

        // ====================== MASTER DATA MANAGEMENT BUTTONS ======================

        private void btnAddPolicyType_Click(object sender, RoutedEventArgs e)
        {
            var window = new PolicyTypeManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            FillPolicyTypeFallback();
        }

        private void btnAddNationality_Click(object sender, RoutedEventArgs e)
        {
            var window = new NationalityManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            FillNationalityFallback();
        }

        private void btnAddSourceOfIncome_Click(object sender, RoutedEventArgs e)
        {
            var window = new SourceOfIncomeManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            FillSourceOfIncomeFallback();
        }

        private void btnAddBusinessActivity_Click(object sender, RoutedEventArgs e)
        {
            var window = new BusinessActivityManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            FillBusinessActivityFallback();
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            var window = new LocationManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            FillLocationFallback();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }
    }

    // ====================== SUPPORTING MODEL CLASSES ======================


}
