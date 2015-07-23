
using System.Collections.Generic;
using Utility;
namespace Lighter.BaseService
{
    //[ServiceContract]
    public interface ILighterService
    {
        OperationResult CheckSession();
    }
}
