using KikisCom.Domain.Models;
using KikisCom.Server.Services.Interfaces;
using KikisCom.Server.WorkClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ObjectResult> Login(string username, string password, bool rememberMe)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return new ObjectResult("Invalid login attempt.") { StatusCode = 401 };
                }

                if (user.isDeleted)
                {
                    return new ObjectResult("This account is deactivated.") { StatusCode = 401 };
                }

                // Аутентификация пользователя
                var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    WriteLog.Info($"User {username} logged in.");
                    return new ObjectResult(new { message = "Login successful" }) { StatusCode = 200 };
                }
                else if (result.IsLockedOut)
                {
                    WriteLog.Warn("User account locked out.");
                    return new ObjectResult("Account is locked.") { StatusCode = 401 };
                }
                else if (result.RequiresTwoFactor)
                {
                    return new ObjectResult("Two-factor authentication is required.") { StatusCode = 401 };
                }
                else
                {
                    return new ObjectResult("Invalid login attempt.") { StatusCode = 401 };
                }
            }
            catch (Exception ex)
            {
                WriteLog.Error($"Error during login: {ex}");
                return new ObjectResult("Internal server error") { StatusCode = 500 };
            }
        }

        public async Task<ObjectResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                //WriteLog.Info($"User logged out.");
                return new ObjectResult("Logout successful") { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                WriteLog.Error($"Error during logout {ex}");
                return new ObjectResult("Internal server error") { StatusCode = 500 };
            }
        }
    }
}
