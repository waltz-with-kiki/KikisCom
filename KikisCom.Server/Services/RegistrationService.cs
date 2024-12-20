using KikisCom.DAL.Data;
using KikisCom.Domain.Models;
using KikisCom.Server.Services.Interfaces;
using KikisCom.Server.WorkClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text;
using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace KikisCom.Server.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IMailService _mailService;
        private readonly IEmailSender _emailSender;

        private readonly ApplicationDbContext _db;
        public RegistrationService(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db, IEmailSender emailSender, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _db = db;
            _mailService = mailService;
            _emailSender = emailSender;
        }
        public async Task<(ObjectResult, string)> UserRegistrationAsync(string email, string userName, string password)
        {
            try
            {
                var isFirstUser = !await _userManager.Users.AnyAsync();
                var user = new ApplicationUser { UserName = userName, Email = email};

                if (user.secondName == null) user.secondName = "";
                await _userStore.SetUserNameAsync(user, userName, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var smtp = _db.Mails.FirstOrDefault();
                    if (smtp != null)
                    {
                        await _mailService.SendEmailRegister(email, userName, password, smtp);
                    }
                        await _userManager.AddToRoleAsync(user, Roles.User);

                    WriteLog.Info($"User {email} created a new account.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    _db.SaveChanges();
                    return (new ObjectResult("OK") { StatusCode = 200 }, email);
                }
                var errors = result.Errors.Select(e => e.Description).ToList();

                return (new ObjectResult(new { Message = "User creation failed", Errors = errors, }) { StatusCode = 400 }, "");
            }
            catch (Exception ex)
            {
                WriteLog.Error($"UserRegistrationAsync method error: {ex}");
                return (new ObjectResult("Fatal server error") { StatusCode = 500 }, "");
            }
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
