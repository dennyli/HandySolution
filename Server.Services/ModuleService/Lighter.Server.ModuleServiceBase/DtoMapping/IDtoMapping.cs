using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Lighter.ModuleServiceBase.DtoMapping
{
    public interface IDtoMapping
    {
        void InitializeMapping<TKey>();

        bool CheckIdIsValid<TKey>(TKey src);

        void LoadEntity<TSource, TMember, TKey>(IMemberConfigurationExpression<TSource> opt,
            Func<TSource, TKey> getId, Func<TKey, TMember> doLoad) where TMember : class;

        //void IgnoreDtoIdAndVersionPropertyToEntity<TKey>();
    }
}
