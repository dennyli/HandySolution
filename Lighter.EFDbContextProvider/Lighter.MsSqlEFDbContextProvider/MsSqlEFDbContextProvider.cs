using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lighter.Server.Infrastructure;
using System.ComponentModel.Composition;

namespace EFDbContextProvider
{
    [Export(typeof(IEFDbContextProvider))]
    public class MsSqlEFDbContextProvider : EFDbContextProviderBase
    {
        public MsSqlEFDbContextProvider()
        {
            SetConnectionParameter("System.Data.SqlClient", "XSLCL", "xsmbscgl");
        }

    }
}
