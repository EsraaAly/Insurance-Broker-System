namespace InsuranceBrokerSystem.UI.Views.Financial
{
    public partial class AccountFormWindow : Window
    {
        public Account EditingAccount { get; private set; }
        public bool IsSaved { get; private set; } = false;
        private readonly System.Collections.Generic.List<AccountType> _takenTypes;

        public AccountFormWindow(Account account, bool isNew = false, System.Collections.Generic.List<AccountType> takenTypes = null)
        {
            InitializeComponent();
            EditingAccount = account;
            _takenTypes = takenTypes ?? new System.Collections.Generic.List<AccountType>();
            DataContext = EditingAccount;
            
            this.cmbAccountType.ItemsSource = Enum.GetValues(typeof(AccountType));

            if (account.Level == 1 && account.CanDelete)
            {
                cmbAccountType.IsEnabled = true;
            }
            else
            {
                cmbAccountType.IsEnabled = false;
            }
            if (isNew)
            {
                txtTitle.Text = "Add New Account";
            }
            else
            {
                txtTitle.Text = $"Edit {account.AccountNumber} - {account.AccountName}";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EditingAccount.AccountName))
            {
                MessageBox.Show("Account Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

             if (EditingAccount.Level == 1 && _takenTypes.Contains(EditingAccount.Type))
            {
                MessageBox.Show($"A root account for '{EditingAccount.Type}' already exists. Root accounts must have unique types.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            IsSaved = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsSaved = false;
            this.Close();
        }
    }
}
