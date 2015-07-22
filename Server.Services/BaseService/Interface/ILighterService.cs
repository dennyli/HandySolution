
using System.Collections.Generic;
using Utility;
namespace Lighter.BaseService.Interface
{
    //[ServiceContract]
    public interface ILighterService
    {
        OperationResult CheckSession();
    }
}
