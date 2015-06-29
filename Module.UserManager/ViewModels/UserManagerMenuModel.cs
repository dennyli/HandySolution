using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModuleBase.ViewModels;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;
using ModuleBase.Services;
using Module.Common;

namespace Module.UserManager.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserManagerMenuModel : MenuModelBase
    {
        [ImportingConstructor]
        public UserManagerMenuModel(IDataService dataService, IEventAggregator eventAggregator, IServiceLocator serviceLocator)
            : base(dataService, eventAggregator, serviceLocator)
        {
        }

        //public override IModuleResources GetModuleResources()
        //{
        //    return new UserManagerResources();
        //}
    }
}
