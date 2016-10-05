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
        /// <summary>
        /// Delete the logfile
        /// </summary>
        public static void DeleteLog()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt");
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
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
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
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
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
