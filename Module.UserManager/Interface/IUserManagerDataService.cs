using Client.Module.Common.Interface;
using System.Collections.ObjectModel;
using Lighter.UserManagerService.Model;
using Client.Module.UserManager.Models;

namespace Client.Module.UserManager.Interface
{
    public interface IUserManagerDataService : IDataService
    {
        Accounts GetAccounts();

        //ObservableCollection<DepartmentDTO> GetDepartments();
        //ObservableCollection<RoleDTO> GetRoles();
    }
}
