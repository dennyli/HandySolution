using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace Lighter.Server.ViewModel
{
    public class HistoryMessageViewModel : NotificationObject
    {
        public HistoryMessageViewModel(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            HistoryMessages = _viewModel.HistoryMessages;

            Title = "历史消息列表";
        }

        private MainViewModel _viewModel;
        /// <summary>
        /// 历史消息
        /// </summary>
        public ObservableCollection<string> HistoryMessages { get; private set; }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Title { get; private set; }
    }
}
