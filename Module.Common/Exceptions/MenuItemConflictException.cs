using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Client.Module.Common.Exceptions
{
    public class MenuItemConflictException : Exception
    {
        public MenuItemConflictException()
            : base()
        {
        }

        public MenuItemConflictException(string message)
            : base(message)
        {
        }

        public MenuItemConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MenuItemConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
