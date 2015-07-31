using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Module.Common.Interface;
using Client.Module.UserManager.Models;

namespace Client.Module.UserManager.Interface.Services
{
    public interface IDepartmentManagerDataService : IDataService
    {
        Departments GetDepartments();
    }
}
