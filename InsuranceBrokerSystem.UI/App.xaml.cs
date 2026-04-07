using System.Configuration;
using System.Data;
using System.Windows;

namespace InsuranceBrokerSystem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Initialize Mappings
            InsuranceBrokerSystem.Application.Common.Mapping.MappingConfig.Configure();
            InsuranceBrokerSystem.UI.Common.Mapping.UIMappingConfig.Configure();
        }
    }

}
