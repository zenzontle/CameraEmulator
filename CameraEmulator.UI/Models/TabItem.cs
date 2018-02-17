using System;
using System.Threading.Tasks;
using CameraEmulator.UI.Services;
using CameraEmulator.UI.ViewModels;
using Catel;
using Catel.MVVM;

namespace CameraEmulator.UI.Models
{
    public class TabItem : ITabItem
    {
        public TabItem(ITaskCommandViewModel viewModel)
        {
            ViewModel = viewModel;
            CanClose = true;

            if (!viewModel.IsClosed)
            {
                viewModel.ClosedAsync += OnViewModelClosed;
            }
        }

        public ITaskCommandViewModel ViewModel { get; private set; }

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
