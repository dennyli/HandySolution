using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Lighter.MainService.Interface;
using Microsoft.Practices.Prism.MefExtensions;

namespace Lighter.MainService.Implement
{
    //public 
        class LighterServiceHost : ServiceHost, ILighterServiceHost
    {
        private LighterServiceHostBootstrapper _bootstrapper;

        protected override void OnOpening()
        {
            ConfigLighterServiceHost();

            base.OnOpening();
        }

        public void ConfigLighterServiceHost()
        {
            _bootstrapper = new LighterServiceHostBootstrapper();
            _bootstrapper.Run();
        }
    }
}
