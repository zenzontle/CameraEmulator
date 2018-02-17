using System;
using CameraEmulator.Core.Scanners;

namespace CameraEmulator.Core
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

        public void SendCaseCode(string code)
        {
            try
            {
                _caseConnector.Send(code);
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
                _sleeveConnector.Send(code);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            _sleeveConnector.Send(code);
        }

        public void SendItemCode(string code)
        {
            try
            {
                _itemConnector.Send(code);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
