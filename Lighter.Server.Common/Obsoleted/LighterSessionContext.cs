using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lighter.Server.Common
{
    public class LighterSessionContext
    {
        private static LighterSessionContext _instance = null;
        public static LighterSessionContext GetInstance()
        {
            if (_instance == null)
                _instance = new LighterSessionContext();

            return _instance;
        }

        public LighterSessionContext()
        {
            SessionManager = new LighterSessionStateManager();
        }

        public LighterSessionStateManager SessionManager;

    }
}
