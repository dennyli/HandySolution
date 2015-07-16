using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

namespace Client.Module.Common.Commands
{
    public class ToolbarCommand : INotifyPropertyChanged
    {
        public ToolbarCommand(ICommand command, string icon, string title)
            : base()
        {
            Command = command;
            Icon = icon;
            Title = title;

            ResetBackBrushToDefault();
        }

        public ICommand Command { get; private set; }

        public string Icon { get; private set; }

        public string Title { get; private set; }

        private Brush _backBrush;
        public Brush BackBrush
        {
            get { return _backBrush; }
            private set
            {
                if (_backBrush != value)
                {
                    _backBrush = value;
                    NotifyPropertyChanged("BackBrush");
                }
            }
        }

        public void SetBackBrushToSelected()
        {
            BackBrush = Brushes.Goldenrod;
        }

        public void ResetBackBrushToDefault()
        {
            BackBrush = Brushes.WhiteSmoke;
        }
        

        //private Boolean _checked = false;
        //public Boolean Checked
        //{
        //    get { return _checked; }
        //    set
        //    {
        //        if (_checked != value)
        //        {
        //            _checked = value;
        //            NotifyPropertyChanged("Checked");
        //        }
        //    }
        //}

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
