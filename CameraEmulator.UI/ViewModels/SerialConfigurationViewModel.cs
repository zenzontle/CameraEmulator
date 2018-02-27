using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using CameraEmulator.Core;
using CameraEmulator.Core.Scanners;
using CameraEmulator.UI.Models;
using CameraEmulator.UI.Services.Configuration;
using Catel.Data;
using Catel.MVVM;

namespace CameraEmulator.UI.ViewModels
{
    public class SerialConfigurationViewModel : ViewModelBase
    {
        private IConfigurationService _configurationService;
        public SerialConfigurationViewModel(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            SaveConfiguration = new TaskCommand(OnSaveConfigurationTask);
            CancelConfiguration = new TaskCommand(OnCancelConfigurationTask);
            Model = new SerialConfigurationModel();
        }

        public IEnumerable<SerialType> SerialTypeValues
        {
            get
            {
                IEnumerable<SerialType> values = Enum.GetValues(typeof(SerialType)).Cast<SerialType>().ToList();
                values = values.Where(x => x != SerialType.Undefined);
                return values;
            }
        }

        public IEnumerable<Parity> ParityValues
        {
            get
            {
                IEnumerable<Parity> values = Enum.GetValues(typeof(Parity)).Cast<Parity>().ToList();
                return values;
            }
        }

        public IEnumerable<StopBits> StopBitsValues
        {
            get
            {
                IEnumerable<StopBits> values = Enum.GetValues(typeof(StopBits)).Cast<StopBits>().ToList();
                return values;
            }
        }

        public override string Title { get; protected set; } = "Serial Configuration";

        [Model]
        public SerialConfigurationModel Model { get; }

        public TaskCommand SaveConfiguration { get; }
        private Task OnSaveConfigurationTask()
        {
            try
            {
                _configurationService.SaveConfiguration();
                return Task.FromResult(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TaskCommand CancelConfiguration { get; }
        private Task OnCancelConfigurationTask()
        {
            //_controller.SendCaseCode(Model.CaseCode);
            return Task.FromResult(0);
        }
    }
}
