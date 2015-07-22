using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Server.Common
{
    public class LighterServerContext
    {
        private static LighterServerContext _instance = null;
        public static LighterServerContext GetInstance()
        {
            if (_instance == null)
                _instance = new LighterServerContext();

            return _instance;
        }

        public LighterServerContext()
        {
            SessionManager = new LighterSessionStateManager();
        }

        public LighterSessionStateManager SessionManager;

    }
}
