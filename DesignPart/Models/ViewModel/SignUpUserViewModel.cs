using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesignPart.Models.ViewModel
{
    public class SignUpUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        [Remote(action: "UserNameIsExist",controller: "Account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please enter mobile number")]
        [Display(Name = "Mobile Number")]
        public long? Mobile { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
