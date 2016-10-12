using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using birthday_reminder.Model;

namespace birthday_reminder.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private InformationList _informations;
        private ICollectionView _informationView;

        public MainViewModel()
        {
            _informations = Database.GetInformations();
            _informationView = CollectionViewSource.GetDefaultView(_informations);
        }

        public InformationList informations
        {
            get { return _informations; }
            set
            {
                if (value != _informations)
                {
                    _informations = value;
                    RaisePropertyChanged("informations");
                    informationView = CollectionViewSource.GetDefaultView(informations);
                }
            }
        }

        public ICollectionView informationView
        {
            get { return _informationView; }
            set
            {
                if (value != _informationView)
                {
                    _informationView = value;
                    RaisePropertyChanged("informationView");
                }
            }
        }
    }
}
