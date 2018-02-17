using System;
using CameraEmulator.Core.Scanners;
using CameraEmulator.UI.Properties;

namespace CameraEmulator.UI.Services.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public bool SaveConfiguration(Scanner caseScanner, Scanner sleeveScanner, Scanner itemScanner)
        {
            try
            {
                Settings.Default.Save();
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error storing user settings");
                return false;
            }
        }
    }
}
