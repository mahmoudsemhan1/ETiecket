using eTickets.date.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile is Required")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Full Name ")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]

        public string FullName { get; set; }


        [Display(Name = " Biography ")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Bio { get; set; }

        //relation

        public List<Movie>? Movies { get; set; }

    }
}
