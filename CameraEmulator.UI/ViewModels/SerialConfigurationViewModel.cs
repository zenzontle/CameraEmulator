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
    public class SerialConfigurationViewModel : ViewModelBase, ITaskCommandViewModel
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
        public SerialConfigurationModel Model { get; private set; }

        public TaskCommand SaveConfiguration { get; }
        private async Task OnSaveConfigurationTask()
        {
            try
            {
                Scanner caseScanner = new Scanner
                {
                    ScannerType = SerialType.Case,
                    BaudRate = Model.CaseScanner.BaudRate,
                    DataBits = Model.CaseScanner.DataBits,
                    Parity = Model.CaseScanner.Parity,
                    PortName = Model.CaseScanner.PortName,
                    StopBits = Model.CaseScanner.StopBits
                };

                Scanner sleeveScanner = new Scanner
                {
                    ScannerType = SerialType.Sleeve,
                    BaudRate = Model.SleeveScanner.BaudRate,
                    DataBits = Model.SleeveScanner.DataBits,
                    Parity = Model.SleeveScanner.Parity,
                    PortName = Model.SleeveScanner.PortName,
                    StopBits = Model.SleeveScanner.StopBits
                };
                Scanner itemScanner = new Scanner
                {
                    ScannerType = SerialType.Item,
                    BaudRate = Model.ItemScanner.BaudRate,
                    DataBits = Model.ItemScanner.DataBits,
                    Parity = Model.ItemScanner.Parity,
                    PortName = Model.ItemScanner.PortName,
                    StopBits = Model.ItemScanner.StopBits
                };

                _configurationService.SaveConfiguration(caseScanner, sleeveScanner, itemScanner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TaskCommand CancelConfiguration { get; }
        private async Task OnCancelConfigurationTask()
        {
            //_controller.SendCaseCode(Model.CaseCode);
        }

        public TaskCommand SaveChanges { get; }
        public TaskCommand ApplyChanges { get; }
        public TaskCommand CancelChanges { get; }

        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }

        public void UpdateModel(SerialConfigurationModel serialConfiguration)
        {
            Model = serialConfiguration;
        }
    }
}
