using Catel.MVVM;

namespace CameraEmulator.UI.ViewModels
{
    public class DashboardViewModel : ViewModelBase, ITaskCommandViewModel
    {
        public DashboardViewModel()
        {
            Title = "Dashboard";
        }
        public TaskCommand SaveChanges { get; }
        public TaskCommand ApplyChanges { get; }
        public TaskCommand CancelChanges { get; }
    }
}
