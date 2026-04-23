using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InsuranceBrokerSystem.Application.DTOs.Client;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Bank;
using InsuranceBrokerSystem.Application.DTOs.Master_Table.Position;
using InsuranceBrokerSystem.Domain.Enums.Client;
using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;
using InsuranceBrokerSystem.UI.Views.MasterData;

namespace InsuranceBrokerSystem.UI.Views.Clients
{
    /// <summary>
    /// Interaction logic for EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        private readonly IServiceContainer _service;
        private GetClientDTO _currentClient;
        public ObservableCollection<ContactItem> Contacts { get; set; }
        public ObservableCollection<DocumentItem> Documents { get; set; }
        public ObservableCollection<BankAccountItem> BankAccounts { get; set; }
        public ObservableCollection<GetBankDTO> Banks { get; set; }
        public ObservableCollection<GetPositionDTO> Positions { get; set; }

        public EditClient()
        {
            InitializeComponent();
            _service = new ServiceContainer(new HttpClientService());
            Contacts = new ObservableCollection<ContactItem>();
            Documents = new ObservableCollection<DocumentItem>();
            BankAccounts = new ObservableCollection<BankAccountItem>();
            Banks = new ObservableCollection<GetBankDTO>();
            Positions = new ObservableCollection<GetPositionDTO>();
            
            DataContext = this;
            InitializeCollections();
            PopulateComboBoxes();
            LoadBanksAndPositions();
        }

        public EditClient(GetClientDTO client) : this()
        {
            _currentClient = client;
            LoadClientData();
        }

        private void InitializeCollections()
        {
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
                // Policy Type - Fetch from database via API with fallback
                CombPolicyType.Items.Clear();
                try
                {
                    var policyTypesResponse = await _service.PolicyTypeApiService.GetAllPolicyTypesAsync();
                    if (policyTypesResponse.Successed && policyTypesResponse.Data != null)
                    {
                        foreach (var policyType in policyTypesResponse.Data)
                        {
                            CombPolicyType.Items.Add(new ComboBoxItem { Content = policyType.Name, Tag = policyType.Id });
                        }
                    }
                    else
                    {
                        FillPolicyTypeFallback();
                    }
                }
                catch (Exception ex)
                {
                    FillPolicyTypeFallback();
                }

                // Nationality - Fetch from database via API with fallback
                CombNationality.Items.Clear();
                try
                {
                    var nationalitiesResponse = await _service.NationalityApiService.GetAllNationalitiesAsync();
                    if (nationalitiesResponse.Successed && nationalitiesResponse.Data != null)
                    {
                        foreach (var nationality in nationalitiesResponse.Data)
                        {
                            CombNationality.Items.Add(new ComboBoxItem { Content = nationality.Name, Tag = nationality.Id });
                        }
                    }
                    else
                    {
                        FillNationalityFallback();
                    }
                }
                catch (Exception ex)
                {
                    FillNationalityFallback();
                }

                // Source of Income - Fetch from database via API with fallback
                CombSourceofIncome.Items.Clear();
                try
                {
                    var sourceOfIncomesResponse = await _service.SourceOfIncomeApiService.GetAllSourceOfIncomesAsync();
                    if (sourceOfIncomesResponse.Successed && sourceOfIncomesResponse.Data != null)
                    {
                        foreach (var source in sourceOfIncomesResponse.Data)
                        {
                            CombSourceofIncome.Items.Add(new ComboBoxItem { Content = source.Name, Tag = source.Id });
                        }
                    }
                    else
                    {
                        FillSourceOfIncomeFallback();
                    }
                }
                catch (Exception ex)
                {
                    FillSourceOfIncomeFallback();
                }

                // Business Activity - Fetch from database via API with fallback
                cmbBusinessActivity.Items.Clear();
                try
                {
                    var businessActivitiesResponse = await _service.BusinessActivityApiService.GetAllBusinessActivitiesAsync();
                    if (businessActivitiesResponse.Successed && businessActivitiesResponse.Data != null)
                    {
                        foreach (var activity in businessActivitiesResponse.Data)
                        {
                            cmbBusinessActivity.Items.Add(new ComboBoxItem { Content = activity.Name, Tag = activity.Id });
                        }
                    }
                    else
                    {
                        FillBusinessActivityFallback();
                    }
                }
                catch (Exception ex)
                {
                    FillBusinessActivityFallback();
                }

                // Location - Fetch from database via API with fallback
                CombLocation.Items.Clear();
                try
                {
                    var locationsResponse = await _service.LocationApiService.GetAllLocationsAsync();
                    if (locationsResponse.Successed && locationsResponse.Data != null)
                    {
                        foreach (var location in locationsResponse.Data)
                        {
                            CombLocation.Items.Add(new ComboBoxItem { Content = location.Name, Tag = location.Id });
                        }
                    }
                    else
                    {
                        FillLocationFallback();
                    }
                }
                catch (Exception ex)
                {
                    FillLocationFallback();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from database: {ex.Message}", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void FillPolicyTypeFallback()
        {
            var fallbackData = new List<string>
            {
                "Life Insurance",
                "Health Insurance", 
                "Motor Insurance",
                "Property Insurance",
                "Travel Insurance",
                "Business Insurance"
            };
            foreach (var item in fallbackData)
            {
                CombPolicyType.Items.Add(new ComboBoxItem { Content = item, Tag = item });
            }
        }

        private void FillNationalityFallback()
        {
            var fallbackData = new List<string>
            {
                "Egyptian",
                "Saudi Arabian",
                "Emirati",
                "Kuwaiti",
                "Qatari",
                "Jordanian",
                "Lebanese",
                "American",
                "British",
                "French"
            };
            foreach (var item in fallbackData)
            {
                CombNationality.Items.Add(new ComboBoxItem { Content = item, Tag = item });
            }
        }

        private void FillSourceOfIncomeFallback()
        {
            var fallbackData = new List<string>
            {
                "Salary",
                "Business",
                "Investment",
                "Rental",
                "Pension",
                "Freelance",
                "Commission",
                "Other"
            };
            foreach (var item in fallbackData)
            {
                CombSourceofIncome.Items.Add(new ComboBoxItem { Content = item, Tag = item });
            }
        }

        private void FillBusinessActivityFallback()
        {
            var fallbackData = new List<string>
            {
                "Trading",
                "Manufacturing",
                "Services",
                "Construction",
                "Technology"
            };
            foreach (var item in fallbackData)
            {
                cmbBusinessActivity.Items.Add(new ComboBoxItem { Content = item, Tag = item });
            }
        }

        private void FillLocationFallback()
        {
            var fallbackData = new List<string>
            {
                "Cairo",
                "Alexandria",
                "Giza",
                "Sharm El Sheikh",
                "Hurghada",
                "Dubai",
                "Riyadh"
            };
            foreach (var item in fallbackData)
            {
                CombLocation.Items.Add(new ComboBoxItem { Content = item, Tag = item });
            }
        }

        private void PopulateOtherComboBoxes()
        {
            // Registration Status
            ComboRegistrationStatus.Items.Clear();
            ComboRegistrationStatus.Items.Add("Joint Liability Company");
            ComboRegistrationStatus.Items.Add("Limited Partnership Company");
            ComboRegistrationStatus.Items.Add("Joint Venture");
            ComboRegistrationStatus.Items.Add("Joint Stock");
            ComboRegistrationStatus.Items.Add("Limited Liability Company");

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
            ComboRegistrationStatus.IsEnabled = isCorporate;
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
                ComboRegistrationStatus.SelectedIndex = -1;
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

        private void LoadClientData()
        {
            if (_currentClient == null) return;

            // Load main client data
            txtClientName.Text = _currentClient.ClientName ?? string.Empty;
            txtClientNameAr.Text = _currentClient.ClientNameAr ?? string.Empty;
            txtOfficialName.Text = _currentClient.OfficialName ?? string.Empty;
            txtIDNo.Text = _currentClient.IdentityNo ?? string.Empty;
            txtCommercialNo.Text = _currentClient.CommercialRegistrationNo ?? string.Empty;

            // Set Client Type selection
            foreach (ComboBoxItem item in CmbProspectType.Items)
            {
                if ((int)item.Tag == _currentClient.ClientType)
                {
                    CmbProspectType.SelectedItem = item;
                    break;
                }
            }
            txtVatNo.Text = _currentClient.VATNo ?? string.Empty;
            txtEmail.Text = _currentClient.Email ?? string.Empty;
            txtTele.Text = _currentClient.Tele ?? string.Empty;
            txtFax.Text = _currentClient.Fax ?? string.Empty;
            txtBuildingNo.Text = _currentClient.BuildingNo ?? string.Empty;
            txtDistrict.Text = _currentClient.District ?? string.Empty;
            txtProspPoBox.Text = _currentClient.POBox ?? string.Empty;
            txtSponsorID.Text = _currentClient.SponsorId ?? string.Empty;
            txtUnifiedNo.Text = _currentClient.UnifiedNo ?? string.Empty;
            txtCapital.Text = _currentClient.Capital?.ToString() ?? string.Empty;
            txtIBANNumber.Text = _currentClient.IBANNumber ?? string.Empty;
            txtIdExpirydateHijir.Text = _currentClient.IDExpiryDateHijri ?? string.Empty;
            txtDateofIncorporationHijri.Text = _currentClient.DateOfIncorporationHijri ?? string.Empty;
            txtExpiryHijri.Text = _currentClient.CRExpiryDateHijri ?? string.Empty;

            // Load dates
            if (_currentClient.IDExpiryDate.HasValue)
                DTPIDExpiryDate.SelectedDate = _currentClient.IDExpiryDate.Value;
            if (_currentClient.DateOfIncorporation.HasValue)
                DTPIncorporation.SelectedDate = _currentClient.DateOfIncorporation.Value;
            if (_currentClient.CRExpiryDate.HasValue)
                DTPExpiryDate.SelectedDate = _currentClient.CRExpiryDate.Value;
            if (_currentClient.DateOfBirth.HasValue)
                txtBirthDay.SelectedDate = _currentClient.DateOfBirth.Value;

            // Load contacts
            if (_currentClient.Contacts != null)
            {
                foreach (var contact in _currentClient.Contacts)
                {
                    Contacts.Add(new ContactItem
                    {
                        Name = contact.Name,
                        PositionId = contact.PositionId,
                        Extension = contact.Extension,
                        Mobile = contact.Mobile,
                        Tele = contact.Tele,
                        Email = contact.Email,
                        SaddadInvoice = contact.SaddadInvoice,
                        Branch = contact.Branch
                    });
                }
            }

            // Load documents
            if (_currentClient.Documents != null)
            {
                int docIndex = 1;
                foreach (var doc in _currentClient.Documents)
                {
                    Documents.Add(new DocumentItem
                    {
                        Index = docIndex++,
                        DocumentType = doc.DocumentType,
                        FileName = doc.FileName,
                        FilePath = doc.FilePath,
                        UploadDate = doc.UploadDate.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd")
                    });
                }
            }

            // Load bank accounts
            if (_currentClient.BankAccounts != null)
            {
                foreach (var bank in _currentClient.BankAccounts)
                {
                    BankAccounts.Add(new BankAccountItem
                    {
                        BankId = bank.BankId,
                        Branch = bank.Branch,
                        IBAN = bank.IBAN,
                        SwiftCode = bank.SwiftCode
                    });
                }
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentClient == null)
                {
                    MessageBox.Show("No client data loaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var updateDto = new UpdateClientDTO
                {
                    Id = _currentClient.Id,
                    ClientName = txtClientName.Text,
                    ClientNameAr = txtClientNameAr.Text,
                    OfficialName = txtOfficialName.Text,
                    IdentityNo = txtIDNo.Text,
                    CommercialRegistrationNo = txtCommercialNo.Text,
                    VATNo = txtVatNo.Text,
                    Email = txtEmail.Text,
                    Tele = txtTele.Text,
                    Fax = txtFax.Text,
                    BuildingNo = txtBuildingNo.Text,
                    District = txtDistrict.Text,
                    POBox = txtProspPoBox.Text,
                    SponsorId = txtSponsorID.Text,
                    UnifiedNo = txtUnifiedNo.Text,
                    Capital = decimal.TryParse(txtCapital.Text, out var capital) ? capital : null,
                    IBANNumber = txtIBANNumber.Text,
                    IDExpiryDate = DTPIDExpiryDate.SelectedDate,
                    DateOfIncorporation = DTPIncorporation.SelectedDate,
                    CRExpiryDate = DTPExpiryDate.SelectedDate,
                    DateOfBirth = txtBirthDay.SelectedDate,
                    IDExpiryDateHijri = txtIdExpirydateHijir.Text,
                    DateOfIncorporationHijri = txtDateofIncorporationHijri.Text,
                    CRExpiryDateHijri = txtExpiryHijri.Text,
                    Contacts = Contacts.Select(c => new UpdateClientContactDTO
                    {
                        Name = c.Name,
                        PositionId = c.PositionId,
                        Extension = c.Extension,
                        Mobile = c.Mobile,
                        Tele = c.Tele,
                        Email = c.Email,
                        SaddadInvoice = c.SaddadInvoice,
                        Branch = c.Branch
                    }).ToList(),
                    Documents = Documents.Select(d => new UpdateClientDocumentDTO
                    {
                        DocumentType = d.DocumentType,
                        FileName = d.FileName,
                        FilePath = d.FilePath,
                        UploadDate = DateTime.Parse(d.UploadDate)
                    }).ToList(),
                    BankAccounts = BankAccounts.Select(b => new UpdateClientBankAccountDTO
                    {
                        BankId = b.BankId,
                        Branch = b.Branch,
                        IBAN = b.IBAN,
                        SwiftCode = b.SwiftCode
                    }).ToList(),
                };

                var result = await _service.ClientService.UpdateClientAsync(updateDto);
                if (result.Successed)
                {
                    MessageBox.Show("Client updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Notify parent window to refresh
                    var parentWindow = Window.GetWindow(this);
                    parentWindow?.Close();
                }
                else
                {
                    MessageBox.Show($"Error updating client: {result.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating client: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFields();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this);
            parentWindow?.Close();
        }

        private void ClearAllFields()
        {
            txtClientName.Clear();
            txtClientNameAr.Clear();
            txtOfficialName.Clear();
            txtIDNo.Clear();
            txtCommercialNo.Clear();
            txtVatNo.Clear();
            txtEmail.Clear();
            txtTele.Clear();
            txtFax.Clear();
            txtBuildingNo.Clear();
            txtDistrict.Clear();
            txtProspPoBox.Clear();
            txtSponsorID.Clear();
            txtUnifiedNo.Clear();
            txtCapital.Clear();
            txtIBANNumber.Clear();
            txtIdExpirydateHijir.Clear();
            txtDateofIncorporationHijri.Clear();
            txtExpiryHijri.Clear();
            DTPIDExpiryDate.SelectedDate = null;
            DTPIncorporation.SelectedDate = null;
            DTPExpiryDate.SelectedDate = null;
            txtBirthDay.SelectedDate = null;
            Contacts.Clear();
            Documents.Clear();
            BankAccounts.Clear();
        }

        private void btnAddContact_Click(object sender, RoutedEventArgs e)
        {
            Contacts.Add(new ContactItem
            {
                Name = "New Contact Person",
                PositionId = null,
                Extension = "",
                Mobile = "",
                Tele = "",
                Email = "",
                Branch = "Riyadh"
            });
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
                foreach (string fileName in dialog.FileNames)
                {
                    var fileInfo = new FileInfo(fileName);
                    Documents.Add(new DocumentItem
                    {
                        Index = Documents.Count + 1,
                        FileName = System.IO.Path.GetFileName(fileName),
                        FilePath = fileName,
                        DocumentType = System.IO.Path.GetExtension(fileName).ToUpper(),
                        FileSize = FormatFileSize(fileInfo.Length),
                        UploadDate = DateTime.Now.ToString()
                    });
                }
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ContactItem contact)
            {
                Contacts.Remove(contact);
            }
        }

        private void DeleteBank_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is BankAccountItem bank)
            {
                BankAccounts.Remove(bank);
            }
        }

        private void DeleteDocument_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is DocumentItem document)
            {
                Documents.Remove(document);
            }
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string filePath)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        MessageBox.Show("File not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DropZone_Drop(object sender, DragEventArgs e)
        {
            // TODO: Implement drag and drop functionality
            MessageBox.Show("Drag and Drop functionality to be implemented.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DropZone_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void DropZone_DragLeave(object sender, DragEventArgs e)
        {
            // TODO: Implement drag leave visual feedback
        }

        private long? GetFileSize(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return new FileInfo(filePath).Length;
                }
            }
            catch
            {
                // Ignore file access errors
            }
            return null;
        }

        // ====================== MASTER DATA MANAGEMENT BUTTONS ======================

        private void btnAddPolicyType_Click(object sender, RoutedEventArgs e)
        {
            var window = new PolicyTypeManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            PopulateComboBoxes();
        }

        private void btnAddNationality_Click(object sender, RoutedEventArgs e)
        {
            var window = new NationalityManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            PopulateComboBoxes();
        }

        private void btnAddSourceOfIncome_Click(object sender, RoutedEventArgs e)
        {
            var window = new SourceOfIncomeManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            FillSourceOfIncomeFallback();
        }

        private async void btnAddBank_Click(object sender, RoutedEventArgs e)
        {
            // Since BankManagementWindow is now a UserControl, it can't be shown as a dialog
            // This functionality should be moved to the main navigation or a different approach
            // For now, we'll just refresh the bank combobox
            await FillBankFallback();
        }

        private async void btnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            // Since PositionManagementWindow is now a UserControl, it can't be shown as a dialog
            // This functionality should be moved to the main navigation or a different approach
            // For now, we'll just refresh the position combobox
            await FillPositionFallback();
        }

        private async Task LoadBanksAndPositions()
        {
            try
            {
                // Load Banks
                var banksResponse = await _service.BankApiService.GetAllBanksAsync();
                if (banksResponse != null)
                {
                    Banks.Clear();
                    if (banksResponse.Data != null)
                    {
                        foreach (var bank in banksResponse.Data)
                        {
                            Banks.Add(bank);
                        }
                    }
                }

                // Load Positions
                var positionsResponse = await _service.PositionApiService.GetAllPositionsAsync();
                if (positionsResponse != null)
                {
                    Positions.Clear();
                    if (positionsResponse.Data != null)
                    {
                        foreach (var position in positionsResponse.Data)
                        {
                            Positions.Add(position);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading banks and positions: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task FillBankFallback()
        {
            // Refresh banks from API
            await LoadBanksAndPositions();
        }

        private async Task FillPositionFallback()
        {
            // Refresh positions from API
            await LoadBanksAndPositions();
        }

        private void btnAddBusinessActivity_Click(object sender, RoutedEventArgs e)
        {
            var window = new BusinessActivityManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            PopulateComboBoxes();
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            var window = new LocationManagementWindow();
            window.Owner = this;
            window.ShowDialog();
            
            // Refresh combobox after closing management window
            PopulateComboBoxes();
        }
    }

    // Helper classes for data binding
    public class ContactItem
    {
        public string Name { get; set; }
        public int? PositionId { get; set; }
        public string Extension { get; set; }
        public string Mobile { get; set; }
        public string Tele { get; set; }
        public string Email { get; set; }
        public bool SaddadInvoice { get; set; }
        public string Branch { get; set; }
    }

    public class DocumentItem
    {
        public int Index { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string UploadDate { get; set; }
    }

    public class BankAccountItem
    {
        public int? BankId { get; set; }
        public string Branch { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }
    }
}
