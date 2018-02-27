using System;
using System.IO.Ports;
using CameraEmulator.Core.Scanners;

namespace CameraEmulator.Core
{
    public class SerialConnector : IDisposable
    {
        private readonly SerialPort _port;

        public event EventHandler<string> DataReceived;

        public SerialConnector(Scanner scanner) : this(scanner.PortName, scanner.BaudRate, scanner.Parity, scanner.DataBits, scanner.StopBits)
        { }

        public SerialConnector(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _port = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                WriteTimeout = 3000
            };

            _port.DataReceived += port_DataReceived;
        }

        public void Connect()
        {
            _port.Open();
        }

        public void Send(string data)
        {
            if (!_port.IsOpen)
            {
                _port.Open();
            }
            _port.Write(data);
        }

        public void Disconnect()
        {
            _port.Close();
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string value = _port.ReadExisting();
            EventHandler<string> handler = DataReceived;
            handler?.Invoke(this, value);
        }

        public void Dispose()
        {
            _port?.Dispose();
        }
    }
}
