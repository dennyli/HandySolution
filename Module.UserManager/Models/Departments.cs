using System.Collections.ObjectModel;
using Lighter.UserManagerService.Model;
using System.Collections.Generic;

namespace Client.Module.UserManager.Models
{
    public class Departments : ObservableCollection<DepartmentDTO>
    {
        public Departments(IList<DepartmentDTO> dtos)
            : base(dtos)
        {
        }
    }
}
