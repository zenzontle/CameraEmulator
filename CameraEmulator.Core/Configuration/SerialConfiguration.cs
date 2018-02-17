using CameraEmulator.Core.Scanners;

namespace CameraEmulator.Core.Configuration
{
    public class SerialConfiguration
    {
        public SerialConfiguration()
        {
            CaseScanner = new Scanner { ScannerType = SerialType.Case };
            SleeveScanner = new Scanner { ScannerType = SerialType.Sleeve };
            ItemScanner = new Scanner { ScannerType = SerialType.Item };
        }

        public Scanner CaseScanner { get; set; }
        public Scanner SleeveScanner { get; set; }
        public Scanner ItemScanner { get; set; }
    }
}
