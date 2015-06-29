using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public class CommonUtility
    {
        public static string GetErrorMessageFromException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            Exception inner_e = ex;
            while (inner_e != null)
            {
                if (sb.Length > 0)
                    sb.Insert(0, "\n");
                sb.Insert(0, inner_e.Message);

                inner_e = inner_e.InnerException;
            }
            sb.Insert(0, "添加记录遇到错误: \n");

            return sb.ToString();
        }
    }
}
