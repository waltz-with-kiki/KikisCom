using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KikisCom.Server.Models.ApiModels.Kiki
{
    public class RegisterInputModel
    {
        [Required]
        public string firstName { get; set; } = "";
        public string? secondName { get; set; } = "";
        [Required]
        public string lastName { get; set; } = "";
        [Required]
        public string UserName { get; set; } = "";
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть не меньше {2} и не больше {1} символов.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = "";
        public bool IsAdmin { get; set; } = false;
    }
}
