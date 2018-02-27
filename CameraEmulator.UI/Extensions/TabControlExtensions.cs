using System.Windows.Controls;

namespace CameraEmulator.UI.Extensions
{
    public static class TabControlExtensions
    {
        public static bool RemoveAndUpdateSelection(this TabControl tabControl, object tabItem)
        {
            var index = tabControl.Items.IndexOf(tabItem);
            if (index == -1)
            {
                return false;
            }

            var wasSelected = ReferenceEquals(tabControl.SelectedItem, tabItem);

            tabControl.Items.RemoveAt(index);

            if (wasSelected)
            {
                var newIndex = index;
                if (newIndex < tabControl.Items.Count)
                {
                    tabControl.Items.MoveCurrentToPosition(newIndex);
                }
                else
                {
                    tabControl.Items.MoveCurrentToLast();
                }
            }

            return true;
        }
    }
}
