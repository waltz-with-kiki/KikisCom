using KikisCom.Server.Data;

namespace KikisCom.Server.WorkClasses
{
    public static class AppConfig
    {
        private static string path = "";
        public static bool ReadConfig()
        {
            bool success = false;

            try
            {
                if (!Directory.Exists($"{path}Config"))
                {
                    Directory.CreateDirectory($"{path}Config");
                    Console.WriteLine($"{DateTime.Now} - Create \"{path}Config\" directory");
                }

                if (File.Exists(path + "Config/config.conf"))
                {
                    string[] configStr = File.ReadAllLines(path + "Config/config.conf");

                    if (configStr.Length > 0)
                    {
                        string sqlServerAdr = "";
                        string sqlServerPort = "";
                        string dataBase = "";
                        string userId = "";
                        string password = "";

                        foreach (string str in configStr)
                        {
                            if (str != "" && str[0] != '#')
                            {
                                if (str.Contains("sqlServerAdr"))
                                {
                                    sqlServerAdr = str.Split('=')[1];
                                }
                                if (str.Contains("sqlServerPort"))
                                {
                                    sqlServerPort = str.Split('=')[1];
                                }
                                if (str.Contains("dataBase"))
                                {
                                    dataBase = str.Split('=')[1];
                                }
                                if (str.Contains("userId"))
                                {
                                    userId = str.Split('=')[1];
                                }
                                if (str.Contains("password"))
                                {
                                    password = str.Split('=')[1];
                                }
                            }
                        }

                            if (sqlServerPort != "")
                            {
                                Immutable.sqlConnect = $"Server={sqlServerAdr},{sqlServerPort}; Database={dataBase}; User ID={userId}; Password={password};";
                            }
                            else
                            {
                                Immutable.sqlConnect = $"Server={sqlServerAdr}; Database={dataBase}; User ID={userId}; Password={password};";
                            }

                        success = true;
                        WriteLog.Info("initialization completed");
                    }
                    else
                    {
                        Console.WriteLine("Config file is empty, server is stoped");
                        WriteLog.Info("Config file is empty, server is stoped");
                    }
                }
                else
                {
                    var logFile = new FileInfo($"{path}Config/config.conf");
                    FileStream fileStream = logFile.Create();
                    fileStream.Close();
                    WriteLog.Warn("Empty config.conf file created");
                    Console.WriteLine("Empty config.conf file created");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] - ReadConfig method error, see logFile");
                WriteLog.Error($"ReadConfig method error, message: {ex.Message}");
            }

            return success;
        }
    }
}
