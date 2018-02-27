namespace CameraEmulator.UI.Models
{
    public class SerialConfigurationModel
    {
        public SerialConfigurationModel()
        {
            CaseScanner = new ScannerModel();
            SleeveScanner = new ScannerModel();
            ItemScanner = new ScannerModel();
        }

        public ScannerModel CaseScanner { get; set; }
        public ScannerModel SleeveScanner { get; set; }
        public ScannerModel ItemScanner { get; set; }
        public int CodeSendDelay { get; set; }
    }
}
