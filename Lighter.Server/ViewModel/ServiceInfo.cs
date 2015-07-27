using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Server.ViewModel
{
    public class ServiceInfo
    {
        public ServiceInfo(string name, Uri uri)
        {
            this.Name = name;
            this.Uri = uri.ToString();
        }

        public string Name { get; private set; }
        public string Uri { get; private set; }
    }
}
