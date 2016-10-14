using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using birthday_reminder.Model;

namespace birthday_reminder.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private InformationList _informations;
        private ICollectionView _informationView;
        private bool _isInformationSelected;
        private bool _isSelectedInNotification;
        private Information _selectedInformation;
        private ICommand _removeNotificationCommand;
        private ICommand _deleteInformationCommand;

        public MainViewModel()
        {
            _informations = Database.GetInformations();
            _informationView = CollectionViewSource.GetDefaultView(_informations);
            _isInformationSelected = false;
            _selectedInformation = null;
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

        public bool isInformationSelected
        {
            get { return _isInformationSelected;}
            set
            {
                if (value != _isInformationSelected)
                {
                    _isInformationSelected = value;
                    RaisePropertyChanged("isInformationSelected");
                }
            }
        }

        public bool isSelectedInNotification
        {
            get { return _isSelectedInNotification;}
            set
            {
                if (value != _isSelectedInNotification)
                {
                    _isSelectedInNotification = value;
                    RaisePropertyChanged("isSelectedInNotification");
                }
            }
        }

        public Information selectedInformation
        {
            get {  return _selectedInformation;}
            set
            {
                if (value != _selectedInformation)
                {
                    _selectedInformation = value;
                    isInformationSelected = _selectedInformation != null;
                    isSelectedInNotification = _selectedInformation?.notification ?? false;
                    RaisePropertyChanged("selectedInformation");
                }
            }
        }

        private void removeNotification()
        {
            Database.RemoveNotification(selectedInformation.getID());
            informations = Database.GetInformations();
        }

        public ICommand removeNotificationCommand
        {
            get {
                return _removeNotificationCommand ??
                       (_removeNotificationCommand = new RelayCommand(param => removeNotification()));
            }
        }

        private void deleteInformation()
        {
            Database.Delete(selectedInformation.getID());
            informations = Database.GetInformations();
        }

        public ICommand deleteInformationCommand
        {
            get
            {
                return _deleteInformationCommand ??
                       (_deleteInformationCommand = new RelayCommand(param => deleteInformation()));
            }
        }
    }
}
