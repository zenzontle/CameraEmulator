using CameraEmulator.Core.Scanners;

namespace CameraEmulator.UI.Services.Configuration
{
    public interface IConfigurationService
    {
        bool SaveConfiguration(Scanner caseScanner, Scanner sleeveScanner, Scanner itemScanner);
    }
}
