﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//		如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 RoleConfigurationAppend 分部方法。
// </auto-generated>
//
// <copyright file="RoleConfiguration.generated.cs">
//		Copyright(c)2013 CodeStar.All rights reserved.
//		CLR版本：4.0.30319.239
//		开发组织：李春林@中国
//		公司网站：http://www.codestar.com
//		所属工程：Lighter.Data
//		生成时间：2015-07-23 16:59
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Linq;

using Lighter.Server.Infrastructure;

namespace Lighter.Data.Repositories
{
	/// <summary>
    ///   仓储操作层实现——角色信息
    /// </summary>
    [Export(typeof(IRoleRepository))]
    public partial class RoleRepository : EFRepositoryBase<Role, String>, IRoleRepository
    { }
}
