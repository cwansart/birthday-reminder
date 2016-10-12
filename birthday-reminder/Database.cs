using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder
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

        public static string GetBirthdaysString()
        {
            CalculateBirthdays();
            var sb = new StringBuilder();
            var command = new SQLiteCommand(
                "select firstname, lastname, day, month, year from information where exists (select information_id from notification where id = information_id);",
                DatabaseConnection.Instance);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sb.Append(Convert.ToString(reader["firstname"])
                    + " " + Convert.ToString(reader["lastname"])
                    + ": " + Convert.ToString(reader["day"]) + "." + Convert.ToString(reader["month"]) + "." +  Convert.ToString(reader["year"])
                    + "\n");
            }
            return sb.ToString();
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
