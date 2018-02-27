using System;
using CameraEmulator.Core.Scanners;

namespace CameraEmulator.Core.Controllers
{
    public class CameraController
    {
        private readonly SerialConnector _caseConnector;
        private readonly SerialConnector _sleeveConnector;
        private readonly SerialConnector _itemConnector;
        public CameraController(Scanner caseScanner, Scanner sleeveScanner, Scanner itemScanner)
        {
            _caseConnector = new SerialConnector(caseScanner);
            _sleeveConnector = new SerialConnector(sleeveScanner);
            _itemConnector = new SerialConnector(itemScanner);
        }

        public string Delimiter { get; set; } = ",";

        public void SendCaseCode(string code)
        {
            try
            {
                _caseConnector.Send($"{code}{Delimiter}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SendSleeveCode(string code)
        {
            try
            {
                _sleeveConnector.Send($"{code}{Delimiter}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SendItemCode(string code)
        {
            try
            {
                _itemConnector.Send($"{code}{Delimiter}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DisconnectAll()
        {
            try
            {
                _caseConnector.Disconnect();
                _sleeveConnector.Disconnect();
                _itemConnector.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DisconnectCase()
        {
            try
            {
                _caseConnector.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DisconnectSleeve()
        {
            try
            {
                _sleeveConnector.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DisconnectItem()
        {
            try
            {
                _itemConnector.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
