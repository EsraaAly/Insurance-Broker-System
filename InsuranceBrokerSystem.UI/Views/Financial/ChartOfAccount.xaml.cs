

using InsuranceBrokerSystem.UI.Interface;
using InsuranceBrokerSystem.UI.Services;

namespace InsuranceBrokerSystem.UI.Views.Financial
{
    /// <summary>
    /// Interaction logic for ChartOfAccount.xaml
    /// </summary>
    public partial class ChartOfAccount : UserControl, INotifyPropertyChanged
    {
        
        private readonly IServiceContainer _service;
        private ObservableCollection<Account> _allAccounts = new ObservableCollection<Account>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set { _selectedAccount = value; OnPropertyChanged(); }
        }

        public ChartOfAccount()
        {

            InitializeComponent();
            DataContext = this;
            _service = new ServiceContainer(new HttpClientService());
            Loaded += async (s, e) => await LoadAccountsAsync();
        }

        private void tvAccounts_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Account account)
            {
                SelectedAccount = account;
            }
        }

        private async Task LoadAccountsAsync()
        {
            try
            {
                var accounts = await _service.ChartOfAccountApiService.LoadAccountsAsync();
                _allAccounts.Clear();

                // Add only root accounts (ParentId == null or 0)
                foreach (var root in accounts.Where(a => a.ParentId == null || a.ParentId == 0))
                {
                    _allAccounts.Add(root);
                }

                tvAccounts.ItemsSource = _allAccounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}");
            }
        }

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            await LoadAccountsAsync();
        }

        private async void BtnAddRoot_Click(object sender, RoutedEventArgs e)
        {
            var newAccount = new Account
            {
                Level = 1,
                IsPostable = false
            };

            var takenTypes = _allAccounts.Select(a => a.Type).ToList();
            var form = new AccountFormWindow(newAccount, isNew: true, takenTypes);
            form.ShowDialog();

            if (form.IsSaved)
            {
                bool success = await _service.ChartOfAccountApiService.AddAccountAsync(form.EditingAccount);
                if (success)
                    await LoadAccountsAsync();
            }
        }

        private async void BtnAddChild_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Account parentAccount)
            {
                var newAccount = new Account
                {
                    ParentId = parentAccount.Id,
                    ParentName = parentAccount.AccountName,
                    Level = parentAccount.Level,
                    Type = parentAccount.Type, 
                    IsPostable = true 
                };

                var form = new AccountFormWindow(newAccount, isNew: true);
                form.ShowDialog();

                if (form.IsSaved)
                {
                    bool success = await _service.ChartOfAccountApiService.AddAccountAsync(form.EditingAccount);
                    if (success)
                        await LoadAccountsAsync();
                }
            }
        }

        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Account accountToEdit = null;
            if (sender is Button btn && btn.Tag is Account tagAccount)
            {
                accountToEdit = tagAccount;
            }
            else if (SelectedAccount != null)
            {
                accountToEdit = SelectedAccount;
            }

            if (accountToEdit != null)
            {
                    bool success = await _service.ChartOfAccountApiService.UpdateAccountAsync(accountToEdit);
                    if (success)
                        await LoadAccountsAsync();              
            }
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Account account)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Delete account '{account.AccountName}' and all children?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool success = await _service.ChartOfAccountApiService.DeleteAccountAsync(account.Id);
                    if (success)
                        await LoadAccountsAsync();
                }
            }
        }

        public Array AccountTypes => Enum.GetValues(typeof(InsuranceBrokerSystem.Domain.Enums.Master_Table.AccountType));

    }

    // Account model for TreeView
    public class Account : INotifyPropertyChanged
    {
        public int Id { get; set; }
        
        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public string Description { get; set; }

        public string ParentName { get; set; }

        private int _level;
        public int Level 
        { 
            get => _level; 
            set { _level = value; OnPropertyChanged(); OnPropertyChanged(nameof(CanAddChild)); } 
        }

        public bool CanAddChild => Level < 4;
        
        public bool CanDelete => Children == null || Children.Count == 0;

        private bool _isExpanded;
        public bool IsExpanded 
        { 
            get => _isExpanded; 
            set { _isExpanded = value; OnPropertyChanged(); } 
        }
        public int? ParentId { get; set; }
        public AccountType Type { get; set; }
        public bool IsPostable { get; set; }

        public ObservableCollection<Account> Children { get; set; } = new ObservableCollection<Account>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}

