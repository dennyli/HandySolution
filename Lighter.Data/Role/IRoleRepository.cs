﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;

namespace Lighter.Data
{
    public interface IRoleRepository : IRepository<Role, string>
    {
    }
}