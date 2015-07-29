using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Client.Module.Common.Commands;

namespace Lighter.Client.Infrastructure.Interface
{
    public interface ICommandContext
    {
        void AddComandInfo(CommandInfo commandInfo);
        void AddComandInfos(ObservableCollection<CommandInfo> commandInfos);

        CommandInfo FindCommandInfoByName(string name);

        IList<CommandInfo> CheckCommandInfosConflict(ObservableCollection<CommandInfo> commandInfos);
        bool CheckCommandInfoConflict(CommandInfo commandnfo);
    }
}
