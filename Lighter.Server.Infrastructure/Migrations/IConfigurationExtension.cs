using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Server.Infrastructure.Migrations
{
    public interface IConfigurationExtension
    {
        void Initialize(EFDbContext context);
    }
}
