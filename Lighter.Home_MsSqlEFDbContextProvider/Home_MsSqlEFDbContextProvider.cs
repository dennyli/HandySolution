using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;
using System.ComponentModel.Composition;
using System.Configuration;

namespace Lighter.EFDbContextProvider
{
    [Export(typeof(IEFDbContextProvider))]
    public class Home_MsSqlEFDbContextProvider : EFDbContextProviderBase
    {
        public Home_MsSqlEFDbContextProvider()
        {
            SetConnectionParameter("System.Data.SqlClient", @"KEVIN-M4800\SQL2014ENT", "LighterDB");
        }
    }
}
