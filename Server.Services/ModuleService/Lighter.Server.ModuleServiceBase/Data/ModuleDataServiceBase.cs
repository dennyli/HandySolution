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
using Lighter.Data.Dto2Entity;
using Lighter.ModuleServiceBase.DtoMapping;

namespace Lighter.ModuleServiceBase.Data
{
    public class ModuleDataServiceBase : LighterServiceBase, IModuleDataServiceBase
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        //[Import]
        //protected IModuleServiceBaseDtoMapping _dtoMapping { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }

        public ModuleDataServiceBase()
        {
            //_dtoMapping.InitializeMapping<string>();
        }

        protected IList<DestinationT> Convert2DTO<SourceT, DestinationT>(IQueryable<SourceT> sources)
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

                IList<SourceT> srcs = sources.ToList<SourceT>();
                IList<DestinationT> dests = srcs.ToDtoList<SourceT, DestinationT>();

                return dests;
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
