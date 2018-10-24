namespace SchoolWeb_DAL.Logging
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    class Logger
    {
        public static string _LogPath;

        public static string SetLogPath(string logPath)
        {
            _LogPath = logPath;

            return _LogPath;
        }

        public static void ExceptionLog(Exception ex, string server = "")
        {
            StreamWriter writer = new StreamWriter(_LogPath, true);
            writer.WriteLine("**************************\n\n");
            writer.WriteLine("{0:MMMM dd, yyyy, hh:mm tt}", DateTime.Now);
            writer.WriteLine($"\n{ex.Message}\n");

            if (server != string.Empty)
            {
                writer.WriteLine(server);
                writer.WriteLine("\n\n");
            }

            writer.WriteLine(ex.StackTrace);

            writer.Close();
            writer.Dispose();

            ex.Data["Logged"] = true;
        }

        public static void SqlExceptionLog(SqlException sqlEx)
        {
            ExceptionLog(sqlEx, sqlEx.Server);
        }
    }
}
