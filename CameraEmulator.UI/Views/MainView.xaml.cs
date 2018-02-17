using CameraEmulator.UI.Services.Interfaces;
using Catel.IoC;

namespace CameraEmulator.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView
    {
        public MainView()
        {
            InitializeComponent();

            var tabService = this.GetServiceLocator().ResolveType<ITabService>();
            tabService?.SetTabControl(TDIControl);
        }
    }
}
