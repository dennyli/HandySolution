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
        public UserManagerModuleInit(IRegionManager regionManager, IEventAggregator eventAggregator, IServiceLocator serviceLocator, ILighterClientContext lighterContext)
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
            IModuleResources resources = GetModuleResources();
            string serviceName = resources.GetServiceName();

            ILighterUserManagerService service = _lighterContext.FindService(serviceName) as ILighterUserManagerService;
            if (service == null)
            {
                ILighterMainService mainService = GetMainService();

                bool bExist = mainService.ServiceIsExists(serviceName);
                if (!bExist)
                    throw new InvalidOperationException("服务端未发现" + serviceName + "服务，无法创建" + serviceName + "服务!");

                Uri[] uris = mainService.GetServiceAddress(serviceName);
                service = ServiceFactory.CreateService<ILighterUserManagerService>(uris[0], _lighterContext);

                _lighterContext.AddService(serviceName, service as ILighterService);
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
