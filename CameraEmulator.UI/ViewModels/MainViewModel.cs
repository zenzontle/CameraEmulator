using System;
using System.ComponentModel;
using CameraEmulator.Core;
using CameraEmulator.UI.Models;
using Catel.MVVM;
using System.Threading.Tasks;
using CameraEmulator.UI.Extensions;
using CameraEmulator.UI.Services.Interfaces;

namespace CameraEmulator.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITabService _tabService;

        public MainViewModel(ITabService tabService)
        {
            _tabService = tabService;
        }
        public override string Title => "CameraEmulator.UI";
        
        protected override async Task InitializeAsync()
        {
            await _tabService.AddAndActivateAsync<DashboardViewModel>(new DashboardViewModel(), false, "Dashboard");

            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
        }
    }
}
