using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Data.Dto2Entity
{
    public interface IDTO2EntityConvertor<TKey>
    {
        void FromDto(DTOEntityBase<TKey> entity);
        DTOEntityBase<TKey> ToDto();
    }
}
