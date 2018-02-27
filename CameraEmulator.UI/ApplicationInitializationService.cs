using System.Threading.Tasks;
using CameraEmulator.UI.ViewModels;
using CameraEmulator.UI.Views;
using Catel.IoC;
using Catel.Logging;
using Catel.MVVM;
using Catel.Windows.Controls;
using Orchestra.Services;

namespace CameraEmulator.UI
{
    public class ApplicationInitializationService : ApplicationInitializationServiceBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        
        public override async Task InitializeBeforeCreatingShellAsync()
        {
            await InitializeCommands();
            
            await ImprovePerformance();

            var viewLocator = ServiceLocator.Default.ResolveType<IViewModelLocator>();
            //viewLocator.Register<RibbonView, RibbonViewModel>();
        }

        private Task InitializeCommands()
        {
            //TODO Register commands
            return Task.FromResult(0);
        }
        
        private Task ImprovePerformance()
        {
            Log.Info("Improving performance");

            UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;
            return Task.FromResult(0);
        }
    }
}
