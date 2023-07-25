using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SIMMS.Modules.ChangePasswordModule.Views;
using SIMMS.Modules.SendCodeModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMMS.Modules.ChangePasswordModule
{
    public class ChangePasswordRegionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentResetRegion", typeof(SendCodeRegion));
            regionManager.RegisterViewWithRegion("ContentReset2", typeof (ChangePasswordRegion));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SendCodeRegion>();
            containerRegistry.RegisterForNavigation<ChangePasswordRegion>();
        }
    }
}
