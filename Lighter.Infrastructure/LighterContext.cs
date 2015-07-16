using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Client.Module.Common.Commands;

namespace Lighter.Client.Infrastructure
{
    public class LighterContext //: INotifyPropertyChanged
    {
        private static LighterContext _instance = null;
        public static LighterContext GetInstance()
        {
            if (_instance == null)
                _instance = new LighterContext();

            return _instance;
        }

        private LighterContext()
        {
        }

        /// <summary>
        /// For All Commands
        /// </summary>
        private ObservableCollection<CommandInfo> _allCommandInfos = new ObservableCollection<CommandInfo>();
        public ObservableCollection<CommandInfo> AllCommandInfos
        {
            get { return _allCommandInfos; }
        }

        public void AddComandInfos(ObservableCollection<CommandInfo> commandInfos)
        {
            foreach (CommandInfo info in commandInfos)
                AddComandInfo(info);
        }

        public void AddComandInfo(CommandInfo commandInfo)
        {
            _allCommandInfos.Add(commandInfo);
        }

        public IList<CommandInfo> CheckCommandInfosConflict(ObservableCollection<CommandInfo> commandInfos)
        {
            IList<CommandInfo> conflictCommands = new List<CommandInfo>();
            foreach (CommandInfo info in commandInfos)
            {
                if (!CheckCommandInfoConflict(info))
                    conflictCommands.Add(info);
            }

            return conflictCommands;
        }

        public bool CheckCommandInfoConflict(CommandInfo commandnfo)
        {
            return _allCommandInfos.Where<CommandInfo>(cmd => (cmd.Name == commandnfo.Name) || (cmd.Key.Key == commandnfo.Key.Key) || cmd.Text == commandnfo.Text).Count<CommandInfo>() > 0;
        }

        ///// <summary>
        ///// INotifyPropertyChanged : PropertyChangedEvent
        ///// </summary>
        //public event PropertyChangedEventHandler PropertyChanged;

        //public void NotifyPropertyChanged(String info)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(info));
        //    }
        //}
    }
}
