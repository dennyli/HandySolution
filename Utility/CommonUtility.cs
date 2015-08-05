using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Utility
{
    public class CommonUtility
    {
        public static string GetErrorMessageFromException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            Exception inner_e = ex;
            while (inner_e != null)
            {
                if (sb.Length > 0)
                    sb.Insert(0, "\n");

                sb2.Remove(0, sb2.Length);
                sb2.Append(inner_e.GetType().FullName);
                sb2.Append("\n");
                sb2.Append(inner_e.Message);

                sb.Insert(0, sb2.ToString());

                inner_e = inner_e.InnerException;
            }
            sb.Insert(0, "遇到错误: \n");

            return sb.ToString();
        }

        public static string GetHostIP4v()
        {
            IPAddress[] addresses = GetHostIPs();
            string[] ips = addresses.Select<IPAddress, string>(ip => ip.ToString()).ToArray<string>();

            return ips[0];
        }

        public static IPAddress[] GetHostIPs()
        {
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());

            return ipe.AddressList.Where<IPAddress>(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToArray<IPAddress>();
        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        private static bool DllIsNetBased(string dllName)
        {
            try
            {
                AssemblyName testAssembly = AssemblyName.GetAssemblyName(dllName);
                return true; 
            }
            catch
            {
            }

            return false;
        }

        public static IList<Assembly> LookupAssemblies(string path, Type serviceIterface)
        {
            IList<Assembly> ass = new List<Assembly>();
            try
            {
                var dlls = from dll in Directory.EnumerateFiles(path, "*.dll") select dll;
                foreach (var dll in dlls)
                {
                    if (DllIsNetBased(dll))
                    {
                        Assembly assembly = Assembly.LoadFrom(dll);
                        foreach (Type type in assembly.GetTypes())
                        {
                            if (type.IsSubclassOf(serviceIterface))
                            {
                                ass.Add(assembly);
                            }
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException UAEx)
            {
                throw new Exception("LookupCommand: " + UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                throw new Exception("LookupCommand: " + PathEx.Message);
            }

            return ass;
        }
    }
}
