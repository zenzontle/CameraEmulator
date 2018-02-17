using System;
using System.Linq;
using System.Threading.Tasks;
using CameraEmulator.UI.Services.Interfaces;
using CameraEmulator.UI.ViewModels;
using Catel.IoC;
using Catel.MVVM;

namespace CameraEmulator.UI.Extensions
{
    public static class TabServiceExtensions
    {
        public static TabItem Add<TViewModel>(this ITabService tabService, object dataContext = null, bool canClose = false, object tag = null)
            where TViewModel : ITaskCommandViewModel, IViewModel
        {
            var tabItem = CreateTabItem<TViewModel>(tabService, dataContext);
            tabItem.CanClose = canClose;
            tabItem.Tag = tag ?? Guid.NewGuid();
            if (tabService.Tabs.All(c => c.Tag != tabItem.Tag))
            {
                tabService.Add(tabItem);
            }
            else
            {
                return tabService.Tabs.Cast<TabItem>().SingleOrDefault(x => x.Tag == tag);
            }


            return tabItem;
        }

        public static async Task<TabItem> AddAndActivateAsync<TViewModel>(this ITabService tabService, object dataContext = null, bool canClose = false, object tag = null)
            where TViewModel : ITaskCommandViewModel, IViewModel
        {
            await Task.Run(() =>
            {
                var tabItem = Add<TViewModel>(tabService, dataContext, canClose, tag);
                tabService.Activate(tabItem);

                return tabItem;
            });

            return null;
        }

        public static TabItem CreateTabItem<TViewModel>(this ITabService tabService, object dataContext) where TViewModel : ITaskCommandViewModel, IViewModel
        {
            TViewModel vm;
            if (dataContext != null)
            {
                vm = (TViewModel)dataContext;
            }
            else
            {
                var dependencyResolver = tabService.GetDependencyResolver();
                var viewModelFactory = dependencyResolver.Resolve<IViewModelFactory>();
                vm = viewModelFactory.CreateViewModel<TViewModel>(dataContext);
            }

            return new TabItem(vm);
        }

        public static void AddAndActivate(this ITabService tabService, TabItem tabItem)
        {
            tabService.Add(tabItem);
            tabService.Activate(tabItem);
        }
    }
}
