using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using birthday_reminder.Model;
using CsvHelper;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;

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
        private ICommand _csvReadCommand;

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

        private void csvRead()
        {
            string path;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                using (TextReader reader = File.OpenText(path))
                {
                    var csv = new CsvReader(reader);
                    while (csv.Read())
                    {
                        try
                        {
                            var firstname = csv.GetField<string>(0);
                            var lastname = csv.GetField<string>(1);
                            var birthday = DateTime.ParseExact(csv.GetField<string>(2), "dd.MM.yyyy", null);
                            Database.Add(firstname, lastname, birthday);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                informations = Database.GetInformations();
            }
        }

        public ICommand csvReadCommand
        {
            get
            {
                return _csvReadCommand ?? (_csvReadCommand = new RelayCommand(param => csvRead()));
            }
        }
    }
}
