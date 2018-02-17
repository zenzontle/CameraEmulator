using System;
using CameraEmulator.UI.ViewModels;

namespace CameraEmulator.UI.Services
{
    public interface ITabItem
    {
        bool CanClose { get; set; }
        object Tag { get; set; }
        ITaskCommandViewModel ViewModel { get; }
        event EventHandler<EventArgs> Closed;
    }
}
