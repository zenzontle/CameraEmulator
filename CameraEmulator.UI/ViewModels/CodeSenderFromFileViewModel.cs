using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CameraEmulator.Core;
using CameraEmulator.Core.Controllers;
using CameraEmulator.UI.Models;
using CameraEmulator.UI.Properties;
using Catel.MVVM;
using Catel.Threading;
using Microsoft.Win32;

namespace CameraEmulator.UI.ViewModels
{
    public class CodeSenderFromFileViewModel : ViewModelBase
    {
        private CodeFileController _controller;

        public CodeSenderFromFileViewModel()
        {
            Model = new LoadFilesModel();
            SendItemCode = new TaskCommand(OnSendItemCodeTask);
            LoadItems = new TaskCommand(OnLoadItems);
            Pause = new TaskCommand(OnPause);

            var caseScanner = Settings.Default.Case;
            var sleeveScanner = Settings.Default.Sleeve;
            var itemScanner = Settings.Default.Item;
            var codeSendDelay = Settings.Default.CodeSendDelay;
            _controller = new CodeFileController(caseScanner, sleeveScanner, itemScanner, codeSendDelay);
        }

        public override string Title { get; protected set; } = "Send Codes From File";

        [Model]
        public LoadFilesModel Model { get; }

        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }
        public int CodesSent { get; set; }
        public string LastCodeSent { get; set; }

        public TaskCommand SendItemCode { get; }
        private Task OnSendItemCodeTask()
        {
            IsRunning = true;
            ThreadPool.QueueUserWorkItem(
                async o =>
                {
                    await _controller.SendItemsFile(Model.CaseCode, Model.SleevesPerCase, Model.ItemsPerSleeve,
                        Model.FullItemsFile, new Progress<CodeProgressInfo>(p =>
                        {
                            CodesSent = p.CodesSent;
                            LastCodeSent = p.LastCodeSent;
                        }));
                    IsRunning = false;
                }
            );
            return Task.FromResult(0);
        }

        public TaskCommand LoadItems { get; }
        private Task OnLoadItems()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                Model.ItemsFile = Path.GetFileName(openFile.FileName);
                Model.FullItemsFile = openFile.FileName;
            }
            return Task.FromResult(0);
        }

        public TaskCommand Pause { get; set; }

        private Task OnPause()
        {
            if (IsPaused)
            {
                //Resume
                _controller.Resume();
                IsPaused = false;
            }
            else
            {
                //Pause
                _controller.Pause();
                IsPaused = true;
            }
            return TaskHelper.Completed;
        }
    }
}
