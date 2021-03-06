﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace birthday_reminder_service
{
    public partial class ServiceServer : ServiceBase
    {
        private Timer timer = null;
        public ServiceServer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.timer = new Timer();
            // Set timer interval to 1 hr (3600000 ms)
            this.timer.Interval = 3600000;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.check_for_birthdays);
            timer.Enabled = true;
            Logger.InitLog();
            Logger.WriteErrorLog("ServerService started");
        }

        protected override void OnStop()
        {
            this.timer.Enabled = false;
            Logger.WriteErrorLog("ServerService stopped");
        }

        /// <summary>
        /// Checks for new birthdays and processes them
        /// </summary>
        private void check_for_birthdays(object sender, ElapsedEventArgs e)
        {
            // TODO: Need to be modified to return only a few of the next birthdays
            var sql = "select firstname, lastname, birthday from information where lastviewed > " + DateTime.Now.Year + ";";
            SQLiteCommand command = new SQLiteCommand(sql, Database.Instance);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // process birtdays
            }
            Logger.WriteErrorLog("Birthdays checked successfully");
        }
    }
}
