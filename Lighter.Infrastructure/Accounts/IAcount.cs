﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Client.Infrastructure.Accounts
{
    internal interface IAcount
    {
        bool CheckHasCommandAuthority(string commandId);
    }
}
