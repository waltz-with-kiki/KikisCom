namespace KikisCom.Server.Data
{
    public class Immutable
    {
        public static DateTime startingTime = DateTime.Now;
        public readonly static string serverVersion = "0.0.0001";
        public static string serverStartString = $"-------------------------------------------\r\n    Start server at {startingTime}\r\n    version: {serverVersion}\r\n-------------------------------------------";
        public readonly static string serverName = "Kikis";
        public static string sqlConnect = "";

        public static string configDir = "";
        public static bool isDocker;
        public static string logsDir = Path.Combine(Environment.CurrentDirectory, "Logs");

        public static bool swagger = true;
        public static string sqlServerName = "";
        public static string sqlConnections = "";
        public static string sqlLogin = "";
        public static string sqlPassword = "";
        public static string sqlBackUpPath = Path.Combine(Environment.CurrentDirectory, "Backup");
        public static string SoftPath = Path.Combine(Environment.CurrentDirectory);
    }
}
