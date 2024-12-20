using System.ComponentModel.DataAnnotations;

namespace KikisCom.Server.Models.ApiModels.Kiki
{
    public class AuthInputModel
    {
        [Required]
        public string UserName { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = false;
    }
}
