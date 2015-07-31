using System;
using System.Windows.Input;
using Client.Module.Common.Interface;

namespace Client.Module.Common.Commands
{
    [Flags]
    public enum CommandAnchor
    {
        MenuItem = 0,
        Toolbar = 1
    }

    public class CommandInfo : ICommandInfo
    {
        public CommandInfo(string id, string name, string text, string icon, KeyGesture key, Type owner, string viewKey, CommandAnchor anchor)
        {
            Id = id;
            Name = name;
            Text = text;
            Key = key;
            Icon = icon;
            ViewKey = viewKey;

            Command = new RoutedUICommand(text,
                            name,
                            owner,
                            new InputGestureCollection(new InputGesture[] { key }));

            CommandBinding = null;

            Anchor = anchor;
        }

        /// <summary>
        /// 命令的唯一标识码，与对应Service中的模块对应，用于确定用户是否有权限操作此命令
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 命令名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 命令显示的文本，包括在菜单项和工具栏中显示
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 命令快捷键定义，如Ctrl+V or Ctrl+Shift+V
        /// </summary>
        public KeyGesture Key { get; private set; }

        /// <summary>
        /// 路由命令 参见<see cref="RoutedUICommand">RoutedUICommand</see>
        /// </summary>
        public ICommand Command { get; private set; }

        /// <summary>
        /// 命令绑定，由系统生成菜单项或工具栏时内部指定，手工设置无效
        /// </summary>
        public CommandBinding CommandBinding { get; set; }

        /// <summary>
        /// 命令图标
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// 指定命令执行时，打开的窗口
        /// </summary>
        public string ViewKey { get; private set; }

        /// <summary>
        /// 指示此命令存在的范围，菜单项、工具栏或二者皆可, 参见<seealso cref="CommandAnchor">CommandAnchor</seealso>.
        /// </summary>
        public CommandAnchor Anchor { get; private set; }
    }
}
