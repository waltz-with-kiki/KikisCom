using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Services.Interfaces
{
    public interface IRegistrationService
    {
        public Task<(ObjectResult, string)> UserRegistrationAsync(string email, string userName, string password);
    }
}
