using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birthday_reminder_service
{
    public static class Logger
    {
        private static readonly string Dir = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\BirthdayReminder";
        /// <summary>
        /// Initialize the logfile
        /// </summary>
        public static void InitLog()
        {
            var di = new DirectoryInfo(Dir);
            if (!di.Exists)
            {
                di.Create();
            }

            if (File.Exists(Dir + "\\LogFile.txt"))
            {
                File.Delete(Dir + "\\LogFile.txt");
            }
        }

        /// <summary>
        /// Write exception to logfile
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(Dir + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " +
                             ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Write message of type string to logfile
        /// </summary>
        /// <param name="message"></param>
        public static void WriteErrorLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(Dir + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
