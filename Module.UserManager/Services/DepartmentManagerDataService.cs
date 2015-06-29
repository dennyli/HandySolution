using System;
using System.ComponentModel.Composition;
using Module.Common.Interface;
using Module.UserManager.Models;
using ModuleBase.Services;
using Module.UserManager.Interface;

namespace Module.UserManager.Services
{
    [Export(typeof(IDepartmentManagerDataService))]
    public class DepartmentManagerDataService : DataServiceBase, IDepartmentManagerDataService
    {
        [ImportingConstructor]
        public DepartmentManagerDataService()
        {
            
        }

        private Departments model = null;
        public override Object GetModel()
        {
#if DEBUG
            if (model == null)
            {
                model = new Departments();

                model.Add(new Department() { Code = "1", Name = "Item 1", Description = "Description 1" });
                model.Add(new Department() { Code = "2", Name = "Item 2", Description = "Description 2" });
                model.Add(new Department() { Code = "3", Name = "Item 3", Description = "Description 3" });
                model.Add(new Department() { Code = "4", Name = "Item 4", Description = "Description 4" });
            }
#else
#endif
            return model;
        }
    }
}
