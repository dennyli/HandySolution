using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Lighter.ModuleServiceBase.DtoMapping
{
    public interface IModuleServiceBaseDtoMapping : IDtoMapping
    {
        void DtoMappingInitialize();
    }
}
