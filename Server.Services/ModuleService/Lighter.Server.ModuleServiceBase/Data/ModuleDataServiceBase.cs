using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using AutoMapper;
using Lighter.BaseService;
using Lighter.Data;
using Lighter.Data.Repositories;
using Lighter.ModuleServiceBase.Model;
using Microsoft.Practices.Prism.Logging;
using Utility;

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

        protected List<DTOEntityBase<string>> Convert2DTO<SourceT, DestinationT>(IQueryable<SourceT> sources) where DestinationT : class where SourceT : class
        {
            try
            {
                List<DTOEntityBase<string>> dtos = new List<DTOEntityBase<string>>();

                List<SourceT> accounts = sources.ToList<SourceT>();
                foreach (SourceT acc in sources)
                {
                    DestinationT dto = Mapper.Map(acc, typeof(SourceT), typeof(DestinationT)) as DestinationT;
                    dtos.Add(dto as DTOEntityBase<string>);
                }

                return dtos;
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
    }
}
