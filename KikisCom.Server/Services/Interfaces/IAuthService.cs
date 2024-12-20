using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ObjectResult> Login(string username, string password, bool rememberMe);
        public Task<ObjectResult> Logout();
    }
}
