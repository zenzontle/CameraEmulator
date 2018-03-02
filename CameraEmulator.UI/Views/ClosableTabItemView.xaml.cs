using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Catel.MVVM;
using Catel.Windows;
using CameraEmulator.UI.Extensions;

namespace CameraEmulator.UI.Views
{
    /// <summary>
    /// Interaction logic for ClosableTabItemView.xaml
    /// </summary>
    public partial class ClosableTabItemView
    {
        public ClosableTabItemView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(ClosableTabItemView), new PropertyMetadata(string.Empty));

        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }

        public static readonly DependencyProperty CanCloseProperty =
            DependencyProperty.Register("CanClose", typeof(bool), typeof(ClosableTabItemView),
                new PropertyMetadata(true));

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var vmContainer = Content as IViewModelContainer;
            if (vmContainer == null)
            {
                return;
            }

            var vm = vmContainer.ViewModel;
            if (vm == null)
            {
                var frameworkElement = vmContainer as FrameworkElement;
                if (frameworkElement != null)
                {
                    vm= frameworkElement.DataContext as IViewModel;
                }

                if (vm == null)
                {
                    return;
                }
            }

            SetBinding(TitleProperty, new Binding
            {
                Source = vm,
                Path = new PropertyPath("Title")
            });
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            if (!CanClose)
            {
                return;
            }

            var tabControl = this.FindVisualAncestorByType<TabControl>();
            tabControl?.RemoveAndUpdateSelection(DataContext);
        }
    }
}
