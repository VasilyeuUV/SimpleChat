using System;
using System.Collections.Generic;
using System.Text;
using Vasilev.SimpleChat.WpfNetCore.Client.ViewModels.Base;

namespace Vasilev.SimpleChat.WpfNetCore.Client.ViewModels
{
    internal class StatusBarViewModel : ViewModelBase
    {

        #region СТАТУС ПРОГРАММЫ
        private string _status = "Статус";

        /// <summary>
        /// Статус программы
        /// </summary>
        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }
        #endregion



    }
}
