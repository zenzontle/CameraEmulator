using System;
using CameraEmulator.UI.Properties;

namespace CameraEmulator.UI.Services.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public bool SaveConfiguration()
        {
            try
            {
                Settings.Default.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
