using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Lighter.Data.Repositories;
using Lighter.Data;
using Lighter.ModuleServiceBase.Model;
using AutoMapper;

namespace Lighter.ModuleServiceBase.Data
{
    public class ModuleDataServiceBase : IModuleDataServiceBase
    {
        [Import]
        protected IAccountRepository AccountRepository { get; set; }

        public IQueryable<Account> Accounts
        {
            get { return AccountRepository.Entities; }
        }

        protected List<DTOEntityBase<string>> Convert2DTO<SourceT, DestinationT>(IQueryable<SourceT> sources) where DestinationT : class where SourceT : class
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
    }
}
