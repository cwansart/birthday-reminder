using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder
{
    class DatabaseConnection
    {
        private static DatabaseConnection connection;
        private static SQLiteConnection dbConnection;
        private static readonly string Dir = AppDomain.CurrentDomain.BaseDirectory;

        private DatabaseConnection()
        {
            try
            {
                if (!File.Exists(Dir + "\\br.db"))
                {
                    SQLiteConnection.CreateFile(Dir + "\\br.db");
                    string sql = "create table information (id integer primary key autoincrement, firstname varchar(40) not null, lastname varchar(40) not null, day int not null, month int not null, year int not null);";
                    SQLiteCommand command = new SQLiteCommand(sql, DatabaseConnection.Instance);
                    command.ExecuteNonQuery();
                    sql = "create table notification (information_id int unique not null, viewed int not null default 0);";
                    command = new SQLiteCommand(sql, DatabaseConnection.Instance);
                    command.ExecuteNonQuery();
                }
                dbConnection = new SQLiteConnection("Data Source=" + Dir + "br.db; Version=3;");
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SQLiteConnection Instance
        {
            get
            {
                if (connection == null)
                {
                    connection = new DatabaseConnection();
                }
                return dbConnection;
            }
        }
    }
}
