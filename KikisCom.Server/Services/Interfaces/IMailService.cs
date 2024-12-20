using KikisCom.Domain.Models;
using KikisCom.Server.Models.ApiModels.Kiki;

namespace KikisCom.Server.Services.Interfaces
{
    public interface IMailService
    {
        public Task SendEmailRegister(string to, string login, string password, Mail smtp);
    }
}
