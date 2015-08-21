using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Lighter.UserManagerService.Model;

namespace Client.Module.UserManager.Models
{
    public class Accounts : ObservableCollection<AccountDTO>
    {
        public Accounts(IList<AccountDTO> dtos)
            : base(dtos)
        {
        }
    }
}
