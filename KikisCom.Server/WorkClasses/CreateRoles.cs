using Microsoft.AspNetCore.Identity;

namespace KikisCom.Server.WorkClasses
{
    public static class CreateRoles
    {
        public static async Task CreateDbRoles(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { Roles.AdminRole, Roles.SuperAdminRole, Roles.User };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            catch(Exception ex)
            {
                WriteLog.Error($"CreateDbRoles method error: {ex}");
            }
        }
    }
}
