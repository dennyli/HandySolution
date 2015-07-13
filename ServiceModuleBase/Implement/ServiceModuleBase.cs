using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceModuleBase.Interface;

namespace ServiceModuleBase
{
    public abstract class ServiceModuleBase : IServiceModule
    {
        public virtual void Initialize();
    }
}
