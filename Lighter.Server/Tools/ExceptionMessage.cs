using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace Lighter.Server.Tools
{
    public class ExceptionMessage
    {
        public static String GetExceptionMessage(Exception ex)
        {
            return CommonUtility.GetErrorMessageFromException(ex);
        }
    }
}
