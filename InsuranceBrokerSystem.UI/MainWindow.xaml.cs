using System.Windows.Media.Animation;

namespace InsuranceBrokerSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new InsuranceBrokerSystem.UI.Views.Dashboard.OperationalDashboard();
        }


        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (sidebarBorder.Width > 0)
            {
                // Get the Collapse animation from Resources
                if (this.Resources["CollapseSidebar"] is Storyboard sbCollapse)
                {
                    sbCollapse.Begin(this);
                }
            }
            else
            {
                // Get the Expand animation from Resources
                if (this.Resources["ExpandSidebar"] is Storyboard sbExpand)
                {
                    sbExpand.Begin(this);
                }
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            string pageName = btn.Content.ToString();
            txtCurrentPage.Text = pageName;

            switch (pageName)
            {
                case "Operational Dashboard":
                case "Dashboard":
                    try
                    {
                        var newView = new InsuranceBrokerSystem.UI.Views.Dashboard.OperationalDashboard();
                        MainContent.Content = newView;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading page: {ex.Message}\n\nInternal: {ex.InnerException?.Message}");
                    }
                    break;
                case "Insurance Classes / Lines of Business":
                    try
                    {
                        // Explicitly use the full namespace if it's in a different folder
                        // Example: var newView = new InsuranceBrokerSystem.UI.Views.InsuranceClassesRegistry();
                        var newView = new InsuranceBrokerSystem.UI.Views.MasterTable.InsuranceClassesRegistry();

                        MainContent.Content = newView;
                    }
                    catch (Exception ex)
                    {
                        // This will pop up a window telling you EXACTLY why it failed
                        MessageBox.Show($"Error loading page: {ex.Message}\n\nInternal: {ex.InnerException?.Message}");
                    }
                    break;
                case "Insurance Companies":
                    try
                    {
                        // Explicitly use the full namespace if it's in a different folder
                        // Example: var newView = new InsuranceBrokerSystem.UI.Views.InsuranceClassesRegistry();
                        var newView = new InsuranceBrokerSystem.UI.Views.MasterTable.InsuranceCompaniesList();

                        MainContent.Content = newView;
                    }
                    catch (Exception ex)
                    {
                        // This will pop up a window telling you EXACTLY why it failed
                        MessageBox.Show($"Error loading page: {ex.Message}\n\nInternal: {ex.InnerException?.Message}");
                    }
                    break;
                case "Chart Of Account":
                    try
                    {
                        // Explicitly use the full namespace if it's in a different folder
                        // Example: var newView = new InsuranceBrokerSystem.UI.Views.InsuranceClassesRegistry();
                        var newView = new InsuranceBrokerSystem.UI.Views.Financial.ChartOfAccount();

                        MainContent.Content = newView;
                    }
                    catch (Exception ex)
                    {
                        // This will pop up a window telling you EXACTLY why it failed
                        MessageBox.Show($"Error loading page: {ex.Message}\n\nInternal: {ex.InnerException?.Message}");
                    }
                    break;
                case "Approve Insurance Companies":
                    try
                    {
                        // Explicitly use the full namespace if it's in a different folder
                        // Example: var newView = new InsuranceBrokerSystem.UI.Views.InsuranceClassesRegistry();
                        var newView = new InsuranceBrokerSystem.UI.Views.Financial.ApproveInsuranceCompany();

                        MainContent.Content = newView;
                    }
                    catch (Exception ex)
                    {
                        // This will pop up a window telling you EXACTLY why it failed
                        MessageBox.Show($"Error loading page: {ex.Message}\n\nInternal: {ex.InnerException?.Message}");
                    }
                    break;

            }
        }
    }
}