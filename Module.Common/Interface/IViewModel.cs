
using System.ServiceModel;
namespace Client.Module.Common.Interface
{
    public interface IViewModel
    {
        void InitilizeServerService<T>(string serviceName, InstanceContext callback);
    }
}
