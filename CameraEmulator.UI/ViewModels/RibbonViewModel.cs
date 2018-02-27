using System.Threading.Tasks;
using CameraEmulator.UI.Extensions;
using CameraEmulator.UI.Services;
using CameraEmulator.UI.Services.Interfaces;
using CameraEmulator.UI.Views;
using Catel.Data;
using Catel.MVVM;

namespace CameraEmulator.UI.ViewModels
{
    public class RibbonViewModel : ViewModelBase
    {
        private readonly ITabService _tabService;
        public RibbonViewModel(ITabService tabService)
        {
            ConfigureCameras = new TaskCommand(OnConfigureCamerasTask);
            SendCodesManually = new TaskCommand(OnSendCodesManuallyTask);
            SendCodesFromFile = new TaskCommand(OnSendCodesFromFileTask);

            _tabService = tabService;
            _tabService.SelectedTabChanged += _tabService_SelectedTabChanged;
        }

        public override string Title { get; protected set; } = "Camera Emulator";

        public ITabItem CurrentTab { private set; get; }

        public TaskCommand ConfigureCameras { get; }
        private async Task OnConfigureCamerasTask()
        {
            await _tabService.AddAndActivateAsync<SerialConfigurationViewModel>(canClose: true,
                tag: "SerialConfiguration");
        }

        public TaskCommand SendCodesManually { get; }
        private async Task OnSendCodesManuallyTask()
        {
            await _tabService.AddAndActivateAsync<CodeSenderManuallyViewModel>(canClose: true, tag: "CodeSenderManually");
        }

        public TaskCommand SendCodesFromFile { get; }
        private async Task OnSendCodesFromFileTask()
        {
            await _tabService.AddAndActivateAsync<CodeSenderFromFileViewModel>(canClose: true, tag: "CodeSenderFromFile");
        }

        private void _tabService_SelectedTabChanged(object sender, TabItemEventArgs e)
        {
            if (e.SelectedTabItem == null)
            {
                CurrentTab = null;
            }
            else
            {
                CurrentTab = !Equals(e.SelectedTabItem.Tag, "Dashboard") ? e.SelectedTabItem : null;
            }
        }
    }
}
