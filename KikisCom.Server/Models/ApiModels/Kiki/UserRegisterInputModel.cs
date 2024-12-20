using System.ComponentModel.DataAnnotations;

namespace KikisCom.Server.Models.ApiModels.Kiki
{
    public class UserRegisterInputModel
    {
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
    }
}
