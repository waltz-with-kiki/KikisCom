using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Services.Interfaces
{
    public interface IAdminPanelService
    {
        public Task<(ObjectResult, string)> AdminRegistrationAsync(string email, string userName, string password, string firstName = "", string secondName = "", string lastName = "", bool isAdmin = false);
    }
}
