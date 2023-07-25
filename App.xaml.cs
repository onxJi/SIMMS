using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using SIMMS.Modules.ChangePasswordModule.ViewModels;
using SIMMS.Modules.ChangePasswordModule.Views;
using SIMMS.Modules.HomeModule.Views;
using SIMMS.Modules.SendCodeModule.ViewModels;
using SIMMS.Modules.SendCodeModule.Views;
using SIMMS.Views;
using System.Windows;

namespace SIMMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
			//containerRegistry.RegisterForNavigation<ChangePasswordRegion>();
			//containerRegistry.RegisterForNavigation<SendCodeRegion>();
			containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
            containerRegistry.RegisterForNavigation<ChangeDataActivity>();

		}
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Modules.ChangePasswordModule.ChangePasswordRegionModule>();
            moduleCatalog.AddModule<Modules.HomeModule.HomeRegionModule>();
            //moduleCatalog.AddModule<WpfApp1.ModuleCModule>();
        }
        protected override void OnInitialized()
        {
            var login = Container.Resolve<Login>();
            login.Show();
            login.IsVisibleChanged += (s, ev) =>
            {
                if (login.IsVisible == false && login.IsLoaded)
                {
                    base.OnInitialized();
                    login.Close();
                }
            };
            
        }
    }
}
