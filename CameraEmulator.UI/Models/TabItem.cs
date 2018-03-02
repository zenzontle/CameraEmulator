using System;
using System.Threading.Tasks;
using CameraEmulator.UI.Services;
using Catel;
using Catel.MVVM;

namespace CameraEmulator.UI.Models
{
    public class TabItem : ITabItem
    {
        public TabItem(IViewModel viewModel)
        {
            ViewModel = viewModel;
            CanClose = true;

            if (!viewModel.IsClosed)
            {
                viewModel.ClosedAsync += OnViewModelClosed;
            }
        }

        public IViewModel ViewModel { get; }

        public bool CanClose { get; set; }
        public object Tag { get; set; }
        public event EventHandler<EventArgs> Closed;

        private async Task OnViewModelClosed(object sender, ViewModelClosedEventArgs e)
        {
            await Task.Run(() =>
            {
                var vm = ViewModel;
                if (vm != null)
                {
                    vm.ClosedAsync -= OnViewModelClosed;
                }
                Closed.SafeInvoke(this);
            });
        }
    }
}
