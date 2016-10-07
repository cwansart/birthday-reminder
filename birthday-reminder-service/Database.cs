using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder_service
{
    class Database
    {
        private static Database connection;
        private static SQLiteConnection dbConnection;
        private static readonly string Dir = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\BirthdayReminder";


        private Database()
        {
            try
            {
                if (!File.Exists(Dir + "\\br.db"))
                {
                    SQLiteConnection.CreateFile(Dir + "\\br.db");
                    string sql = "create table information (firstname varchar(40) not null, lastname varchar(40) not null, birthday int not null, lastviewed int not null);";
                    SQLiteCommand command = new SQLiteCommand(sql, Database.Instance);
                    command.ExecuteNonQuery();
                }
                dbConnection = new SQLiteConnection("Data Source=" + Dir + "\\br.db;Version=3;");
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex);
            }
        }

        public static SQLiteConnection Instance
        {
            get
            {
                if (connection == null)
                {
                    connection = new Database();
                }
                return dbConnection;
            }
        }
    }
}
