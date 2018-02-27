using System;
using Catel.MVVM;

namespace CameraEmulator.UI.Services
{
    public interface ITabItem
    {
        bool CanClose { get; set; }
        object Tag { get; set; }
        IViewModel ViewModel { get; }
        event EventHandler<EventArgs> Closed;
    }
}
