using System.Threading.Tasks;
using Catel.Logging;
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
        }

        private Task InitializeCommands()
        {
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
