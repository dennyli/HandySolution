using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;
using System.ComponentModel.Composition;

namespace Lighter.Data
{
    [Export(typeof(IRoleRepository))]
    public class RoleRepository : EFRepositoryBase<Role, string>, IRoleRepository
    {
    }
}
