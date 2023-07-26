using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SIMMS.Modules.DataBaseModule.Views;
using SIMMS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMMS.Modules.DataBaseModule
{
	public class DataBaseModuleRegions : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{
			
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<DataBaseCreateProduct>();
			//containerRegistry.RegisterForNavigation<DataBase>();
		}
	}
}
