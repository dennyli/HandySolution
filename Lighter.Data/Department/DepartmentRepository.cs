using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;
using System.ComponentModel.Composition;

namespace Lighter.Data.Department
{
    [Export(typeof(IDepartmentRepository))]
    public class DepartmentRepository : EFRepositoryBase<Department, string>,  IDepartmentRepository
    {
    }
}
