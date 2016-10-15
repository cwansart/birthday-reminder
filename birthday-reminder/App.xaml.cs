using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using birthday_reminder.Model;

namespace birthday_reminder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        private static Timer timer;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //create the notifyicon
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            ShowNotification(null, null);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }

        private static void ShowNotification(object source, ElapsedEventArgs e)
        {
            timer?.Stop();
            if (Database.GetBirthdays().Count != 0)
            {
                var text = "Geburtstage:\n";
                foreach (var birthday in Database.GetBirthdays())
                {
                    text += birthday.firstname + " " + birthday.lastname + ": " + birthday.birthday.ToString("dd.mm.yy") +
                            " (" + birthday.age + ")\n";
                }
                MessageBox.Show(text);
            }
            var nextNotify = DateTime.Today.AddDays(1).AddSeconds(30);
            timer = new Timer((double)(nextNotify - DateTime.Now).TotalMilliseconds);
            timer.Elapsed += ShowNotification;
            timer.Enabled = true;
        }
    }
}
