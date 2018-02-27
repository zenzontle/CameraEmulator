using System;
using System.Collections.Generic;

namespace CameraEmulator.UI.Services.Interfaces
{
    public interface ITabService
    {
        IEnumerable<ITabItem> Tabs { get; }

        ITabItem SelectedTab { get; }

        event EventHandler<TabItemEventArgs> SelectedTabChanged;

        bool IsVisible(ITabItem tabItem);

        bool IsActive(ITabItem tabItem);

        void Add(ITabItem tabItem);

        void Insert(int index, ITabItem tabItem);

        void Activate(ITabItem tabItem);

        void Remove(ITabItem tabItem);

        void SetTabControl(object tabControl);
    }
}
