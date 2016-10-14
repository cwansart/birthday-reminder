using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder.Model
{
    public class Information : BaseModel
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private DateTime _birthday;
        private bool _notification;
        private int? _age;

        public Information(int id, string firstname, string lastname, DateTime birthday, bool notification)
        {
            this._id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.birthday = birthday;
            this.notification = notification;
            this.age = null;
            if (notification) this.age = calculateAge();
        }

        private int calculateAge()
        {
            var today = DateTime.Today.AddMonths(6);
            var years = today.Year - birthday.Year;
            if (today < birthday.AddYears(years)) years--;
            return years;
        }

        public int getID()
        {
            return _id;
        }

        public string firstname
        {
            get
            {
                return _firstname;
            }
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                    RaisePropertyChanged("firstname");
                }
            }
        }

        public string lastname
        {
            get
            {
                return _lastname;
            }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    RaisePropertyChanged("lastname");
                }
            }
        }

        public DateTime birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    RaisePropertyChanged("birthday");
                }
            }
        }

        public bool notification
        {
            get
            {
                return _notification;
            }
            set
            {
                if (_notification != value)
                {
                    _notification = value;
                    RaisePropertyChanged("notification");
                }
            }
        }

        public int? age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    RaisePropertyChanged("age");
                }
            }
        }
    }
}
