using eTickets.date.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema Logo Is Required")]
        public string Logo { get; set; }
        [Required(ErrorMessage = "Cinema Name Is Required")]
        [Display(Name = "Cinema Name")]

        public string Name { get; set; }
        [Required(ErrorMessage = " Description Is Required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        ///relation

        public List<Movie>? movies { get; set; }
    }
}
