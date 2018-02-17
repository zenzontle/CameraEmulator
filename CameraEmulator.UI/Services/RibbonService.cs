using System.Windows;
using CameraEmulator.UI.Views;
using Orchestra.Services;

namespace CameraEmulator.UI.Services
{
    public class RibbonService : IRibbonService
    {
        public FrameworkElement GetMainView()
        {
            return new MainView();
        }

        public FrameworkElement GetStatusBar()
        {
            return new StatusBarView();
        }

        public FrameworkElement GetRibbon()
        {
            return new RibbonView();
        }
    }
}
