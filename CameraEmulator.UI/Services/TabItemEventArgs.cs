namespace CameraEmulator.UI.Services
{
    public class TabItemEventArgs
    {
        public ITabItem SelectedTabItem { get; set; }

        public TabItemEventArgs(ITabItem selectedTabItem)
        {
            SelectedTabItem = selectedTabItem;
        }
    }
}
