using System.ComponentModel.DataAnnotations;

namespace DesignPart.Models.ViewModel
{
    public class LoginSignUpViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Display(Name = "Remember Me")]
        public bool IsRemember { get; set; }
    }
}
