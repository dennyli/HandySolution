using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.BaseService.Interface;

namespace Lighter.BaseService
{
    public abstract class ServiceModuleBase : ILighterService
    {
        public abstract void Initialize();
    }
}
