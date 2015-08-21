using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Lighter.UserManagerService.Model;

namespace Client.Module.UserManager.Models
{
    public class Roles : ObservableCollection<RoleDTO>
    {
        public Roles(IList<RoleDTO> dtos)
            : base(dtos)
        {
        }
    }
}
