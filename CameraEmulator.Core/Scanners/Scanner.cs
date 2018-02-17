using System.Configuration;
using System.IO.Ports;

namespace CameraEmulator.Core.Scanners
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class Scanner
    {
        public Scanner()
        {
            ScannerType = SerialType.Undefined;
            PortName = "COM";
            BaudRate = 9600;
            Parity = Parity.None;
            DataBits = 8;
            StopBits = StopBits.One;
        }
        public SerialType ScannerType { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
    }
}
