using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Module.Common.Commands;
using Client.Module.Common.Events;
using Client.Module.Common.Interface;
using Client.ModuleBase;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Lighter.MainService.Interface;
using Lighter.UserManagerService.Interface;
using Lighter.BaseService.Interface;
using System.ServiceModel;
using Lighter.Client.Infrastructure.Interface;
using Lighter.Client.Infrastructure.Implement;

namespace Client.Module.UserManager
{
    [ModuleExport("Client.Module.UserManager.UserManagerModuleInit", typeof(IModule))]
    public class UserManagerModuleInit : ModuleBaseInit
    {
        [ImportingConstructor]
        public UserManagerModuleInit(IRegionManager regionManager, IEventAggregator eventAggregator, IServiceLocator serviceLocator, ILighterContext lighterContext)
            : base(regionManager, eventAggregator, serviceLocator, lighterContext)
        {
        }

        #region IModule Members

        public override void Initialize()
        {
            MenuItemAndToolbarInitialize(typeof(UserManagerModuleInit), CommandDefinitions.CU_TOPMENU_TEXT, CommandDefinitions.CU_TOPMENU_TEXT);

            CreateService();
        }

        protected override ILighterService CreateService()
        {
            ILighterUserManagerService service = _lighterContext.FindService("UserManagerService") as ILighterUserManagerService;
            if (service == null)
            {
                ILighterMainService mainService = GetMainService();

                string serviceName = "LighterUserManagerService";
                bool bExist = mainService.ServiceIsExists(serviceName);
                if (!bExist)
                    throw new InvalidOperationException("服务端未发现UserManager服务，无法创建UserManager服务!");

                Uri[] uris = mainService.GetServiceAddress(serviceName);
                service = ServiceFactory<ILighterUserManagerService>.CreateService(uris[0]);

                _lighterContext.AddService("UserManagerService", service);
            }

            return service as ILighterService;
        }

        public override IModuleResources CreateModuleResources()
        {
            return new UserManagerResources();
        }

        #endregion
    }
}
