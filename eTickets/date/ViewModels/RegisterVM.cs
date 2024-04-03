using System.ComponentModel.DataAnnotations;
namespace eTickets.date.ViewModels

{
    public class RegisterVM
    {
        [Display(Name = "Full Name ")]
        [Required(ErrorMessage = "Full Name Address is require")]
        public string FullName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is require")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
