using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;

namespace DataUploader
{
    public class Logger
    {

        public static void log(string message, int id, string list)
        {
            try
            {
                string logLocation = @"C:\root\Test\";

                string fileName = "WorkingLog-" + Regex.Replace(DateTime.Now.ToShortDateString(), @"[^a-zA-Z0-9\-]", "") + ".txt";

                string fullPath = logLocation + @"\WorkingLogs\" + @"\" + list + @"\" + fileName;

                if (!Directory.Exists(logLocation))
                {
                    Directory.CreateDirectory(logLocation);
                }
                if (!Directory.Exists(logLocation + @"\WorkingLogs\"))
                {
                    Directory.CreateDirectory(logLocation + @"\WorkingLogs\");
                }

                
                if (!Directory.Exists(logLocation + @"\WorkingLogs\" +  @"\" + list))
                {
                    Directory.CreateDirectory(logLocation + @"\WorkingLogs\" + @"\" + list);
                }

                if (!File.Exists(fullPath))
                {
                    using (FileStream strm = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {

                        using (StreamWriter sw = new StreamWriter(strm))
                        {


                            sw.WriteLine(" Processing List:" + list + " Processing ID:" + id + " Message:" + message + " DateTime:" + DateTime.Now.ToString() + "\n");


                        }
                    }

                }
                else
                {
                    using (FileStream strm = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                    {

                        using (StreamWriter sw = new StreamWriter(strm))
                        {
                            sw.WriteLine(" Processing List:" + list + " Processing ID:" + id + " Message:" + message + " DateTime:" + DateTime.Now.ToString() + "\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public static void errorlog(string message, int id, string list)
        {
            string logLocation = @"C:\root\Test\";

            string fileName = "ErrorLog-" + Regex.Replace(DateTime.Now.ToShortDateString(), @"[^a-zA-Z0-9\-]", "") + ".txt";

            string fullPath = logLocation + @"\ErrorLogs\" + @"\" + list + @"\" + fileName;

            if (!Directory.Exists(logLocation))
            {
                Directory.CreateDirectory(logLocation);
            }
            if (!Directory.Exists(logLocation + @"\ErrorLogs\"))
            {
                Directory.CreateDirectory(logLocation + @"\ErrorLogs\");
            }

            

            if (!Directory.Exists(logLocation + @"\ErrorLogs\" + @"\" + list))
            {
                Directory.CreateDirectory(logLocation + @"\ErrorLogs\" + @"\" + list);
            }

            if (!File.Exists(fullPath))
            {
                using (FileStream strm = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {

                    using (StreamWriter sw = new StreamWriter(strm))
                    {


                        sw.WriteLine(" Processing List:" + list + " Processing ID:" + id + " Message:" + message + " DateTime:" + DateTime.Now.ToString() + "\n");


                    }
                }

            }
            else
            {
                using (FileStream strm = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
                {

                    using (StreamWriter sw = new StreamWriter(strm))
                    {
                        sw.WriteLine(" Processing List:" + list + " Processing ID:" + id + " Message:" + message + " DateTime:" + DateTime.Now.ToString() + "\n");
                    }
                }
            }

        }





    }
}
