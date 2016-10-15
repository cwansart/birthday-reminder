using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder.Model
{
    public static class Database
    {
        private static string ConvertDate(DateTime date)
        {
            string dateString = Convert.ToDateTime(date).ToString("dd'.'MM'.'yyyy");
            string[] parts = dateString.Split(new char[] { '.' });
            return parts[0] + ", " + parts[1] + ", " + parts[2];
        }

        private static DateTime ConvertDate(int day, int month, int year)
        {
            return new DateTime(year, month, day);
        }

        public static void Add(string firstname, string lastname, DateTime birthday)
        {
            var command = new SQLiteCommand(
                "insert into information (firstname, lastname, day, month, year) values ('" + firstname + "', '" + lastname + "', " + ConvertDate(birthday) + ");",
                DatabaseConnection.Instance);
            command.ExecuteNonQuery();
        }

        public static void RemoveNotification(int id)
        {
            var command = new SQLiteCommand("update notification set viewed = 1 where information_id = '" + id + "';", DatabaseConnection.Instance);
            command.ExecuteNonQuery();
        }

        public static void Delete(int id)
        {
            var command = new SQLiteCommand("delete from information where rowid = " + id + ";", DatabaseConnection.Instance);
            command.ExecuteNonQuery();
            command = new SQLiteCommand("delete from notification where information_id = " + id + ";", DatabaseConnection.Instance);
            command.ExecuteNonQuery();
        }

        public static InformationList GetBirthdays()
        {
            CalculateBirthdays();
            var result = new InformationList();
            var command = new SQLiteCommand(
                "select id, firstname, lastname, day, month, year from information where exists (select information_id from notification where id = information_id and viewed = 0) order by month asc, day asc, firstname asc, lastname asc;",
                DatabaseConnection.Instance);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Information(Convert.ToInt32(reader["id"]), Convert.ToString(reader["firstname"]), Convert.ToString(reader["lastname"]), ConvertDate(Convert.ToInt32(reader["day"]), Convert.ToInt32(reader["month"]), Convert.ToInt32(reader["year"])), true));
            }
            return result;
        }

        public static InformationList GetInformations()
        {
            var result = GetBirthdays();
            var command = new SQLiteCommand(
                "select id, firstname, lastname, day, month, year from information where not exists (select information_id from notification where id = information_id and viewed = 0) order by month asc, day asc, firstname asc, lastname asc;",
                DatabaseConnection.Instance);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Information(Convert.ToInt32(reader["id"]), Convert.ToString(reader["firstname"]), Convert.ToString(reader["lastname"]), ConvertDate(Convert.ToInt32(reader["day"]), Convert.ToInt32(reader["month"]), Convert.ToInt32(reader["year"])), false));
            }
            return result;
        }

        private static void CalculateBirthdays()
        {
            DateTime today = DateTime.Now;
            for (int i = 0; i < 3; i++)
            {
                var command = new SQLiteCommand(
                "INSERT OR IGNORE INTO notification (information_id) select id from information where day = " + today.Day + " and month = " + today.Month + ";",
                DatabaseConnection.Instance);
                command.ExecuteNonQuery();
                today = today.AddDays(1);
            }
        }
    }
}
