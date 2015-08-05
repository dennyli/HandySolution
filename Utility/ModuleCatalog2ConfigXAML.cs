using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using System.IO;

namespace Utility
{
    public class ModuleCatalog2ConfigXAML
    {
        private const string SPACE_STRING_4 = "    ";
        private const string SPACE_STRING_8 = "        ";
        private ModuleCatalog moduleCatalog;
        private StringBuilder sb;

        public ModuleCatalog2ConfigXAML(ModuleCatalog moduleCatalog)
        {
            sb = new StringBuilder();
            this.moduleCatalog = moduleCatalog;
        }

        public void GenerateXAMLConfigFile(string fileFullPath)
        {
            BuildHeader();
            BuildGroups();
            BuildByGroupless();
            BuildFooter();
            SaveAsFile(fileFullPath, sb.ToString());
        }

        private static void SaveAsFile(string savedPath, string content)
        {
            
            using (StreamWriter sw = File.CreateText(savedPath))
            {
                sw.Write(content);
            }
        }

        private void BuildHeader()
        {
            sb.AppendLine("<Modularity:ModuleCatalog xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            sb.AppendLine(SPACE_STRING_8 +"xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"");
            sb.AppendLine(SPACE_STRING_8 + "xmlns:sys=\"clr-namespace:System;assembly=mscorlib\"");
            sb.AppendLine(SPACE_STRING_8 + "xmlns:Modularity=\"clr-namespace:Microsoft.Practices.Prism.Modularity;assembly=Microsoft.Practices.Prism\">");

        }

        private void BuildFooter()
        {
            sb.AppendLine("</Modularity:ModuleCatalog>");
        }

        private void BuildGroups()
        {
            foreach (var group in moduleCatalog.Groups)
            {
                BuildByGroup(group);
            }
        }

        private void BuildByGroup(ModuleInfoGroup group)
        {
            //IEnumerable<ModuleInfoGroup> groups = this.moduleCatalog.Groups;
            sb.AppendLine(SPACE_STRING_4 + "<Modularity:ModuleInfoGroup ");
            if (!string.IsNullOrEmpty(group.Ref))
            {
                sb.Append(" Ref=\"" + group.Ref + "\"");
            }

            sb.Append(" InitializationMode=\"" + group.InitializationMode.ToString() +"\">");
            IEnumerator<ModuleInfo> moduleInfos = group.GetEnumerator();
            
            while(moduleInfos.MoveNext())
            {
                BuildModule(moduleInfos.Current,SPACE_STRING_8);
            }
        }

        private void BuildByGroupless()
        {
            IEnumerable<ModuleInfo> groupLessModules = this.moduleCatalog.Items.OfType<ModuleInfo>();
            foreach (var moduleInfo in groupLessModules)
            {
                BuildModule( moduleInfo,SPACE_STRING_4);
            }
        }

        private void BuildModule(ModuleInfo moduleInfo,string space)
        {
            sb.AppendLine(space + "<!-- Module info without a group -->");

            string content = space + "<Modularity:ModuleInfo ";

            
            if (!string.IsNullOrEmpty(moduleInfo.Ref))
            {
                content += " Ref=\"" + moduleInfo.Ref + "\"";
            }

            if (!string.IsNullOrEmpty(moduleInfo.ModuleName))
            {
                content += " ModuleName=\"" + moduleInfo.ModuleName + "\"";
            }

            content += " ModuleType=\"" + moduleInfo.ModuleType + "\" >";

            sb.AppendLine(content);

            if (moduleInfo.DependsOn != null && moduleInfo.DependsOn.Count>0)
            {
                sb.AppendLine(space + SPACE_STRING_4 + "<Modularity:ModuleInfo.DependsOn>");
                foreach (var item in moduleInfo.DependsOn)
                {
                    sb.AppendLine(space + SPACE_STRING_4 + SPACE_STRING_4 + "<sys:String>" + item + "</sys:String>");
                }

                sb.AppendLine(space + SPACE_STRING_4 + "</Modularity:ModuleInfo.DependsOn>");
                sb.AppendLine(space + "</Modularity:ModuleInfo> ");
            }
           
            sb.AppendLine(space + "</Modularity:ModuleInfo> ");
                //sb.Append(" />");
            
        }
    }
}
