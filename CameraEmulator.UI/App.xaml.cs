using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Windows;
using CameraEmulator.Core;
using CameraEmulator.Core.Scanners;
using CameraEmulator.UI.Properties;
using Catel.ApiCop;
using Catel.ApiCop.Listeners;
using Catel.IoC;
using Catel.Logging;
using Orchestra.Services;
using Orchestra.Views;

namespace CameraEmulator.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
#if DEBUG
            LogManager.AddDebugListener();
#endif
            if (Settings.Default.Case == null)
            {
                Settings.Default.Case = new Scanner {ScannerType = SerialType.Case};
            }
            if (Settings.Default.Sleeve == null)
            {
                Settings.Default.Sleeve = new Scanner { ScannerType = SerialType.Sleeve };
            }
            if (Settings.Default.Item == null)
            {
                Settings.Default.Item = new Scanner { ScannerType = SerialType.Item };
            }

            Log.Info("Starting application");
            
            CultureInfo cultureUI = new CultureInfo(ConfigurationManager.AppSettings["language"] ?? "en-US");
            Thread.CurrentThread.CurrentUICulture = cultureUI;

            var serviceLocator = ServiceLocator.Default;
            var shellService = serviceLocator.ResolveType<IShellService>();
            await shellService.CreateWithSplashAsync<ShellWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            ApiCopManager.AddListener(new ConsoleApiCopListener());
            ApiCopManager.WriteResults();

            base.OnExit(e);
        }
    }
}