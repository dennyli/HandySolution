using System;
using System.Windows.Input;
using Module.Common.Interface;

namespace Module.Common.Commands
{
    [Flags]
    public enum CommandAnchor
    {
        MenuItem = 0,
        Toolbar = 1
    }

    public class CommandInfo : ICommandInfo
    {
        public CommandInfo(string name, string text, string icon, KeyGesture key, Type owner, string viewKey, CommandAnchor anchor)
        {
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

        public string Name { get; private set; }

        public string Text { get; private set; }

        public KeyGesture Key { get; private set; }

        public ICommand Command { get; private set; }

        public CommandBinding CommandBinding { get; set; }

        public string Icon { get; private set; }

        public string ViewKey { get; private set; }

        public CommandAnchor Anchor { get; private set; }
    }
}
