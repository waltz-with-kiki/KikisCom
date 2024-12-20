using KikisCom.DAL.Data;
using KikisCom.Server.Data;
using KikisCom.Server.Services.Interfaces;
using KikisCom.Server.WorkClasses;
using Microsoft.EntityFrameworkCore;

namespace KikisCom.Server.Services
{
    public class DatabaseManagementService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseManagementService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool EnsureDatabaseIsUpToDate()
        {
            if (string.IsNullOrEmpty(Immutable.sqlBackUpPath))
            {
                WriteLog.Error("DatabaseManagementService.EnsureDatabaseIsUpToDate method error, message: BackUp path is empty aborting database update");
                return false;
            }
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    if (dbContext.Database.CanConnect())
                    {
                        Console.WriteLine("Connection to DataBase sucсessful");
                        WriteLog.Info("Connection to DataBase suсcessful");
                        var pendingMigrations = dbContext.Database.GetPendingMigrations().ToList();
                        if (pendingMigrations.Any())
                        {
                            //CreateBackup(dbContext);
                            dbContext.Database.Migrate();
                        }
                        else
                        {
                            Console.WriteLine("No changes to the database");
                            WriteLog.Info("No changes to the database");
                        }
                    }
                    else
                    {
                        Console.WriteLine("A new database created");
                        WriteLog.Info("A new database created");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Error($"DatabaseManagementService.EnsureDatabaseIsUpToDate method error, message: {ex}");
                return false;
            }

            return true;
        }

        private bool CreateBackup(ApplicationDbContext dbContext)
        {
            try
            {
                var backupFileName = $"{Immutable.SoftPath}KikisService_{DateTime.Now:yyyyMMddHHmmss}.bak";

                var sql = $@"BACKUP DATABASE [KikisService] TO DISK = N'{backupFileName}' WITH NOFORMAT, NOINIT, NAME = 'KikisService-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10";

                dbContext.Database.ExecuteSqlRaw(sql);

                return true;
            }
            catch (Exception ex)
            {
                WriteLog.Error($"DatabaseManagementService.CreateBackup method error, message: {ex.Message}");
                return false;
            }
        }
    }
}
