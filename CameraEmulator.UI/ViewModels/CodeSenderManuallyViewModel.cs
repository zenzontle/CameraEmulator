using System.Threading.Tasks;
using CameraEmulator.Core.Controllers;
using CameraEmulator.UI.Models;
using CameraEmulator.UI.Properties;
using Catel.MVVM;

namespace CameraEmulator.UI.ViewModels
{
    public class CodeSenderManuallyViewModel : ViewModelBase
    {
        private CameraController _controller;

        public CodeSenderManuallyViewModel()
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

        public override string Title { get; protected set; } = "Send Codes Manually";

        [Model]
        public CamerasModel Model { get; }


        public TaskCommand SendCaseCode { get; }
        private Task OnSendCaseCodeTask()
        {
            _controller.SendCaseCode(Model.CaseCode);
            _controller.DisconnectCase();
            return Task.FromResult(0);
        }

        public TaskCommand SendSleeveCode { get; }
        private Task OnSendSleeveCodeTask()
        {
            _controller.SendSleeveCode(Model.SleeveCode);
            _controller.DisconnectSleeve();
            return Task.FromResult(0);
        }

        public TaskCommand SendItemCode { get; }
        private Task OnSendItemCodeTask()
        {
            _controller.SendItemCode(Model.ItemCode);
            _controller.DisconnectItem();
            return Task.FromResult(0);
        }
    }
}
