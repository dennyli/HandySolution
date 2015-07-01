﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

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
