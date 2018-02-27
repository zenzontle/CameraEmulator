using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CameraEmulator.UI.Extensions;
using CameraEmulator.UI.Services.Interfaces;
using Catel.Logging;

namespace CameraEmulator.UI.Services
{
    public class TabService : ITabService
    {
        private TabControl _tabControl;

        public IEnumerable<ITabItem> Tabs
        {
            get
            {
                if (_tabControl == null)
                {
                    return new List<ViewModels.TabItem>();
                }
                return (from tab in _tabControl.Items.Cast<object>()
                    where tab is ITabItem
                    select (ITabItem) tab).ToList();
            }
        }

        public ITabItem SelectedTab
        {
            get
            {
                var tabItem = _tabControl?.SelectedItem as ITabItem;
                return tabItem;
            }
        }

        public event EventHandler<TabItemEventArgs> SelectedTabChanged;

        public bool IsVisible(ITabItem tabItem)
        {
            if (_tabControl == null)
            {
                return false;
            }
            var isVisible = (from item in _tabControl.Items.Cast<object>()
                where ReferenceEquals(tabItem, item)
                select true).Any();
            return isVisible;
        }

        public bool IsActive(ITabItem tabItem)
        {
            if (_tabControl == null)
            {
                return false;
            }
            var selectedTab = SelectedTab;
            var isActive = ReferenceEquals(selectedTab, tabItem);
            return isActive;
        }

        public void Add(ITabItem tabItem)
        {
            if (_tabControl == null)
            {
                return;
            }

            Insert(_tabControl.Items.Count, tabItem);
        }

        public void Insert(int index, ITabItem tabItem)
        {
            if (_tabControl == null)
            {
                return;
            }

            var isVisible = IsVisible(tabItem);
            if (isVisible)
            {
                return;
            }

            tabItem.Closed += OnTabItemClosed;

            _tabControl.Dispatcher.BeginInvoke(new Action(() => _tabControl.Items.Insert(index, tabItem)));
        }

        public void Activate(ITabItem tabItem)
        {
            if (_tabControl == null)
            {
                return;
            }

            //var isVisible = IsVisible(tabItem);
            //if (!isVisible)
            //{
            //    throw Log.ErrorAndCreateException<InvalidOperationException>("Tab item is not visible, use the Show() method first");
            //}
            _tabControl.Dispatcher.BeginInvoke(new Action(() => _tabControl.SelectedItem = tabItem));
        }

        public void Remove(ITabItem tabItem)
        {
            if (_tabControl == null)
            {
                return;
            }

            tabItem.Closed -= OnTabItemClosed;

            _tabControl.RemoveAndUpdateSelection(tabItem);
        }

        public void Clear()
        {
            if (_tabControl == null)
            {
                return;
            }
            _tabControl.Dispatcher.BeginInvoke(new Action(() => _tabControl.Items.Clear()));
        }

        public void SetTabControl(object tabControl)
        {
            if (tabControl is TabControl)
            {
                if (_tabControl != null)
                {
                    _tabControl.SelectionChanged -= OnTabControlSelectionChanged;
                }

                _tabControl = (TabControl) tabControl;
                _tabControl.SelectionChanged += OnTabControlSelectionChanged;
            }
        }


        private void OnTabItemClosed(object sender, EventArgs e)
        {
            var tabItem = sender as ViewModels.TabItem;
            if (tabItem != null)
            {
                tabItem.Closed -= OnTabItemClosed;

                if (_tabControl == null)
                {
                    return;
                }

                _tabControl.Dispatcher.BeginInvoke(new Action(() => Remove(tabItem)));
            }
        }

        private void OnTabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var handler = SelectedTabChanged;
            if (handler != null)
            {
                var selectedTab = SelectedTab;
                if (selectedTab != null)
                {
                    handler(this, new TabItemEventArgs(selectedTab));
                }
            }
        }
    }
}