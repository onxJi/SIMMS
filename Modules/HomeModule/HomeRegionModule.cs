using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SIMMS.Modules.ChangePasswordModule.Views;
using SIMMS.Modules.DataBaseModule.Views;
using SIMMS.Modules.SendCodeModule.Views;
using SIMMS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Modules.HomeModule
{
	public class HomeRegionModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			var regionManager = containerProvider.Resolve<IRegionManager>();
			regionManager.RegisterViewWithRegion("ContentRegion", typeof(Home));
			regionManager.RegisterViewWithRegion("ContentRegion", typeof(DataBase));
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<Home>();
			containerRegistry.RegisterForNavigation<DataBase>();
		}
	}
}
