using Orchestra;

namespace CameraEmulator.UI.Views
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    public partial class RibbonView
    {
        public RibbonView()
        {
            InitializeComponent();

            Ribbon.AddAboutButton();
        }
    }
}
