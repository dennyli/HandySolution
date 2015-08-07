using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Lighter.Client.ViewModel
{
    internal class LoginInfoArgs
    {
        public TextBox AccountControl { get; private set; }
        public PasswordBox PasswordControl { get; private set; }
        public string IP { get; private set; }

        public LoginInfoArgs(TextBox ctrlAccount, PasswordBox ctrlPwd, string ip)
        {
            AccountControl = ctrlAccount;
            PasswordControl = ctrlPwd;
            IP = ip;
        }
    }
}
