using System.Collections.Generic;
using AutoMapper;

namespace Lighter.Data.Dto2Entity
{
    public static class AutoMapperCollectionExtension
    {
        public static IList<TDto> ToDtoList<TEntity, TDto>(this IList<TEntity> entityList)
        {
            return Mapper.Map<IList<TEntity>, IList<TDto>>(entityList);
        }
    }
}
