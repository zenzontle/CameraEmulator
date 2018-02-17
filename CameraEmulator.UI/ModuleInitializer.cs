using CameraEmulator.UI;
using CameraEmulator.UI.Services;
using CameraEmulator.UI.Services.Configuration;
using CameraEmulator.UI.Services.Interfaces;
using Catel.IoC;
using Orchestra.Services;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;
        serviceLocator.RegisterType<IRibbonService, RibbonService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationService>();
        serviceLocator.RegisterType<ITabService, TabService>(RegistrationType.Singleton);
        serviceLocator.RegisterType<IConfigurationService, ConfigurationService>();

    }
}