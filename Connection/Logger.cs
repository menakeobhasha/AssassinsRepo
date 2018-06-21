using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class Logger
    {
        private static string logPath = string.Empty;

        public static void Write(string message)
        {
            try
            {
                if (!Common.EnableLogger)
                    return;
                StreamWriter streamWriter = new StreamWriter(Path.Combine(Common.LogFilePath, DateTime.Today.ToString("yyyyMMdd") + "_ErrorLog.log"), true);
                streamWriter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + ": " + message);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static void Write(string filename, string message)
        {
            try
            {
                if (!Common.EnableLogger)
                    return;
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Common.LogFilePath, DateTime.Today.ToString("yyyyMMdd") + "_" + filename + ".log"), true))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + ": " + message);
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void Write(Exception exception)
        {
            try
            {
                if (!Common.EnableLogger)
                    return;
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Common.LogFilePath, DateTime.Today.ToString("yyyyMMdd") + "_ErrorLog.log"), true))
                {
                    streamWriter.WriteLine("Date          : " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"));
                    streamWriter.WriteLine("User          : " + Common.LoggedUser);
                    streamWriter.WriteLine("Message       : " + exception.Message);
                    streamWriter.WriteLine("Source        : " + exception.Source);
                    streamWriter.WriteLine("StackTrace    : " + exception.StackTrace);
                    streamWriter.WriteLine("----------------------------------------------------------------------------------");
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void Write(Exception exception, List<Parameter> parameters)
        {
            try
            {
                if (!Common.EnableLogger)
                    return;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Parameter parameter in parameters)
                    stringBuilder.Append(parameter.Name.ToString()).Append(",").Append(parameter.Value ?? (object)"Null").Append("|");
                string logFilePath = Common.LogFilePath;
                DateTime dateTime = DateTime.Today;
                string path2 = dateTime.ToString("yyyyMMdd") + "_ErrorLog.log";
                using (StreamWriter streamWriter1 = new StreamWriter(Path.Combine(logFilePath, path2), true))
                {
                    StreamWriter streamWriter2 = streamWriter1;
                    string str1 = "Date          : ";
                    dateTime = DateTime.Now;
                    string str2 = dateTime.ToString("yyyy/MM/dd hh:mm:ss");
                    string str3 = str1 + str2;
                    streamWriter2.WriteLine(str3);
                    streamWriter1.WriteLine("User          : " + Common.LoggedUser);
                    streamWriter1.WriteLine("Message       : " + exception.Message);
                    streamWriter1.WriteLine("Source        : " + exception.Source);
                    streamWriter1.WriteLine("StackTrace    : " + exception.StackTrace);
                    streamWriter1.WriteLine("Parameters    : " + stringBuilder.ToString().TrimEnd('|'));
                    streamWriter1.WriteLine("----------------------------------------------------------------------------------");
                    streamWriter1.Flush();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
