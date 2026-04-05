using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InsuranceBrokerSystem.UI.Views.Clients
{
    /// <summary>
    /// Interaction logic for ClientRegistry.xaml
    /// </summary>
    public partial class ClientRegistry : UserControl
    {
        public ClientRegistry()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRegisterNewClient_Click(object sender, RoutedEventArgs e)
        {
            RegisterNewClient popup = new RegisterNewClient();
            popup.ShowDialog();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrintList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
