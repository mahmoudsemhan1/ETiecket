using System.ComponentModel.DataAnnotations;

namespace eTickets.date.ViewModels
{
    public class LoginVm
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage = "Email Address is require")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
