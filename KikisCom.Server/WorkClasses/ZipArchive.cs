using KikisCom.Server.Data;
using System.IO.Compression;

namespace KikisCom.Server.WorkClasses
{
    public class Zip
    {
        public static void AddLogToZip(string fileName)
        {
            string zipFile = Path.Combine(Immutable.logsDir, "logs.zip");

            if (!File.Exists(zipFile))
            {
                using (ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    Console.WriteLine($"[{DateTime.Now}] - Create zip file");
                    WriteLog.Info("Create zip file");
                }
            }

            try
            {
                string[] files = Directory.GetFiles(Immutable.logsDir);

                foreach (string file in files)
                {
                    if (!file.Contains(fileName) && !file.Contains(".zip"))
                    {
                        using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Update))
                        {
                            zipArchive.CreateEntryFromFile(file, file.Substring(Immutable.logsDir.Length + 1));
                            File.Delete(file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Error($"AddLogToZip method error, message: {ex}");
            }
        }
    }
}
