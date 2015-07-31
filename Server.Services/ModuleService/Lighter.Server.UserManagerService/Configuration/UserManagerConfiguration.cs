using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Lighter.Data;
using Lighter.Server.Infrastructure;
using Lighter.Server.Infrastructure.Migrations;
using Lighter.UserManagerService.Defination;

namespace Lighter.UserManagerService.Configuration
{
    [Export(typeof(IConfigurationExtension))]
    public class UserManagerConfiguration : IConfigurationExtension
    {
        public void Initialize(EFDbContext context)
        {
            // Module
            List<Module> modules = new List<Module>()
            {
                new Module() { Id = "MU01", Name="用户管理", Catalog=UserManagerDefination.ServiceId, IsDeleted=false, LastDate=DateTime.Now },
                new Module() { Id = "MU02", Name="部门管理", Catalog=UserManagerDefination.ServiceId, IsDeleted=false, LastDate=DateTime.Now },
                new Module() { Id = "MU03", Name="岗位管理", Catalog=UserManagerDefination.ServiceId, IsDeleted=false, LastDate=DateTime.Now },
                new Module() { Id = "MU04", Name="权限管理", Catalog=UserManagerDefination.ServiceId, IsDeleted=false, LastDate=DateTime.Now },
                new Module() { Id = "MU05", Name="模块管理", Catalog=UserManagerDefination.ServiceId, IsDeleted=false, LastDate=DateTime.Now }
            };
            DbSet<Module> moduleSet = context.Set<Module>();
            moduleSet.AddOrUpdate<Module>(m => new { m.Id }, modules.ToArray());

            // Role
            List<Role> roles = new List<Role>()
            {
                new Role() { Id = "R01", Name="角色01", Authority="MU01MU02MU03", Description="用户管理角色", IsDeleted=false, LastDate=DateTime.Now },
                new Role() { Id = "R02", Name="角色02", Authority="MU01MU03MU04", Description="", IsDeleted=false, LastDate=DateTime.Now },
                new Role() { Id = "R03", Name="角色03", Authority="MU01MU02", Description="", IsDeleted=false, LastDate=DateTime.Now },
                new Role() { Id = "R04", Name="角色04", Authority="MU01MU02MU03MU04", Description="", IsDeleted=false, LastDate=DateTime.Now },
                new Role() { Id = "R05", Name="角色05", Authority="MU01MU02MU03MU04MU05", Description="", IsDeleted=false, LastDate=DateTime.Now }
            };
            DbSet<Role> roleSet = context.Set<Role>();
            roleSet.AddOrUpdate<Role>(r => new { r.Id }, roles.ToArray());

            // Account
            List<Account> accounts = new List<Account>()
            {
                new Account() { Id = "ACC000", Name="Admin", ShortName="Admin", Password="123456", LastDate=DateTime.Now },
                new Account() { Id = "ACC001", Name="Acc001", ShortName="A001", Password="123456", LastDate=DateTime.Now }
            };
            DbSet<Account> accountSet = context.Set<Account>();
            accountSet.AddOrUpdate<Account>(a => new { a.Id }, accounts.ToArray());

            // Department
            List<Department> departments = new List<Department>()
            {
                new Department() { Id="DEPART001", Name="生产部", Description = "生产部门", LastDate=DateTime.Now },
                new Department() { Id="DEPART002", Name="销售部", Description = "销售部门", LastDate=DateTime.Now },
                new Department() { Id="DEPART003", Name="行政部", Description = "行政部门", LastDate=DateTime.Now },
                new Department() { Id="DEPART004", Name="财务部", Description = "财务部门", LastDate=DateTime.Now }
            };
            DbSet<Department> departmentSet = context.Set<Department>();
            departmentSet.AddOrUpdate<Department>(d => new { d.Id }, departments.ToArray());

            // Save changes
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
