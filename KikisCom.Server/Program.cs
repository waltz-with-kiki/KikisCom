using KikisCom.DAL.Data;
using KikisCom.Domain.Models;
using KikisCom.Server.Data;
using KikisCom.Server.Services;
using KikisCom.Server.Services.Interfaces;
using KikisCom.Server.WorkClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Task.Run(() => StartServer(args));
        /*
        if (AppConfig.ReadConfig())
        {
            await Task.Run(() => StartServer(args));
        }
        else
        {
            Console.WriteLine("config file read error");
            WriteLog.Error("config file read error");
        }*/
    }
    public static void StartServer(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var connectionString = "Server=WIN-U7C7HFPBMVS;Database=KikisDb;TrustServerCertificate=True;MultipleActiveResultSets=True;Integrated Security=True;";
        // Add services to the container.

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                              options.UseSqlServer(connectionString));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        })
            .AddDefaultTokenProviders().AddDefaultUI()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IMailService, MailService>();
        builder.Services.AddScoped<IAdminPanelService, AdminPanelService>();
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();

        builder.Services.AddTransient<DatabaseManagementService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var databaseManagementService = scope.ServiceProvider.GetRequiredService<DatabaseManagementService>();
                if (!databaseManagementService.EnsureDatabaseIsUpToDate())
                {
                    Console.WriteLine("Check or update DB error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Server runtime error, cannot connect to DataBase, server was stopped");
                WriteLog.Error($"Server runtime error, cannot connect to DataBase, server was stopped, message: {ex}");
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            CreateRoles.CreateDbRoles(services).GetAwaiter().GetResult();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();



        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (Immutable.swagger)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KikisCom API V1");
                c.RoutePrefix = "swagger";
            });
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();


        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}