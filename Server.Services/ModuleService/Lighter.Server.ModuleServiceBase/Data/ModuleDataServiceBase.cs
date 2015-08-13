using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Lighter.BaseService;
using Lighter.Data;
using Lighter.Data.Repositories;
using Lighter.ModuleServiceBase.Model;
using Microsoft.Practices.Prism.Logging;
using Utility;
using AutoMapper;

namespace Lighter.ModuleServiceBase.Data
{
    public class ModuleDataServiceBase : LighterServiceBase, IModuleDataServiceBase
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }

        public ModuleDataServiceBase()
        {
            Mapper.Initialize(cfg =>
              {
                  cfg.CreateMap<EntityBase<string>, DTOEntityBase<string>>()
                      .Include<Module, ModuleDTO>();

                  cfg.CreateMap<Module, ModuleDTO>();
              });
        }

        protected IQueryable<DestinationT> Convert2DTO<SourceT, DestinationT>(IQueryable<SourceT> sources)
            where DestinationT : class
            where SourceT : class
        {
            try
            {
                //List<DestinationT> dtos = new List<DestinationT>();

                //List<SourceT> accounts = sources.ToList<SourceT>();
                //foreach (SourceT acc in sources)
                //{
                //    DestinationT dto = Mapper.Map<SourceT, DestinationT>(acc);
                //    dtos.Add(dto as DTOEntityBase<string>);
                //}

                //return dtos;

                return Mapper.Map<IQueryable<DestinationT>>(sources);
            }
            catch (Exception ex)
            {
                this.Logger.Log(CommonUtility.GetErrorMessageFromException(ex), Category.Exception, Priority.High);
            }

            return null;
        }

        public override void Initialize()
        {
            
        }

        public override void Dispose()
        {
        }
    }
}
