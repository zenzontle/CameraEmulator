using System.Threading.Tasks;
using CameraEmulator.Core;
using CameraEmulator.UI.Models;
using CameraEmulator.UI.Properties;
using Catel.MVVM;

namespace CameraEmulator.UI.ViewModels
{
    public class CodeSendersViewModel : ViewModelBase, ITaskCommandViewModel
    {
        private CameraController _controller;

        public CodeSendersViewModel()
        {
            Model = new CamerasModel();
            SendCaseCode = new TaskCommand(OnSendCaseCodeTask);
            SendSleeveCode = new TaskCommand(OnSendSleeveCodeTask);
            SendItemCode = new TaskCommand(OnSendItemCodeTask);
            
            var caseScanner = Settings.Default.Case;
            var sleeveScanner = Settings.Default.Sleeve;
            var itemScanner = Settings.Default.Item;
            _controller = new CameraController(caseScanner, sleeveScanner, itemScanner);
        }

        public override string Title { get; protected set; } = "Send Codes";

        [Model]
        public CamerasModel Model { get; private set; }


        public TaskCommand SendCaseCode { get; }
        private async Task OnSendCaseCodeTask()
        {
            _controller.SendCaseCode(Model.CaseCode);
        }

        public TaskCommand SendSleeveCode { get; }
        private async Task OnSendSleeveCodeTask()
        {
            _controller.SendSleeveCode(Model.SleeveCode);
        }

        public TaskCommand SendItemCode { get; }
        private async Task OnSendItemCodeTask()
        {
            _controller.SendItemCode(Model.ItemCode);
        }

        public TaskCommand SaveChanges { get; }
        public TaskCommand ApplyChanges { get; }
        public TaskCommand CancelChanges { get; }
    }
}
