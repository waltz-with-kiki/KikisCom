using KikisCom.Server.Data;
using System.Collections.Concurrent;

namespace KikisCom.Server.WorkClasses
{
    public class WriteLog
    {
        private static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        private static bool[] isProcessing = new bool[2] { false, false };
        private static StreamWriter sw = new StreamWriter(Path.Combine(Immutable.logsDir, GetFileName(DateTime.Now)), true);
        private static object lockObj = new();
        private static bool updateLogFile = false;

        public static void Info(string data)
        {
            AddToQueue(data, " INFO");
        }

        public static void Warn(string data)
        {
            AddToQueue(data, " WARN");
        }

        public static void Error(string data)
        {
            ServerData.errors++;
            AddToQueue(data, "ERROR");
        }

        public static void CheckLogFiles()
        {
            string logsFileName = GetFileName(DateTime.Now);
            string logFilePath = Path.Combine(Immutable.logsDir, logsFileName);

            if (!File.Exists(logFilePath))
            {
                updateLogFile = true;
                lock (lockObj)
                {
                    sw.Close();
                    sw.Dispose();
                    sw = new StreamWriter(logFilePath, true);
                }
                updateLogFile = false;

                string? emptyString = null;
                AddToQueue(emptyString, "EMPTY");
            }

            Zip.AddLogToZip(logsFileName);
        }

        private static void AddToQueue(string? data, string type)
        {
            if (data != null)
            {
                string logItemData = $"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}] {type} - {data}";
                queue.Enqueue(logItemData);
            }

            if (!isProcessing[0])
                Task.Run(() => Write(ref isProcessing[0]));
            else if (!isProcessing[1])
                Task.Run(() => Write(ref isProcessing[1]));
        }

        private static string GetFileName(DateTime date)
        {
            return $"{date.ToString("dd.MM.yyyy")}_log.txt";
        }

        private static void Write(ref bool processing)
        {
            processing = true;

            lock (lockObj)
            {
                while (!updateLogFile && queue.TryDequeue(out string? data) && data != null)
                {
                    try
                    {
                        sw.WriteLine(data);
                        sw.Flush();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }

            processing = false;
        }
    }
}
