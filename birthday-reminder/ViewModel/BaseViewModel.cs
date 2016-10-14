﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder.ViewModel
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string prop)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}