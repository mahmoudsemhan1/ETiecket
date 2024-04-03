using eTickets.date.Base;
using eTickets.date.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieMV
    {
        public int Id { get; set; }
        [Display(Name ="Movie Name")]
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }
        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Poster Is Required")]

        public string ImageURL { get; set; }
        [Display(Name = "Movie Price")]
        [Required(ErrorMessage = "Price Is Required")]

        public double Price { get; set; }
        [Display(Name = " Movie Start Date")]
        [Required(ErrorMessage = "Start Date Is Required")]

        public DateTime StartDate { get; set; }
        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "End Date Is Required")]

        public DateTime EndDate { get; set; }
        [Display(Name = "Select a Category")]
        [Required(ErrorMessage = "Movie Category Is Required")]

        public MovieCategory MovieCategory { get; set; }

        //relation
        [Display(Name = "select Actor(s)")]
        [Required(ErrorMessage = "Start Actor(s) Is Required")]

        public List<int> ActorIds { get; set; }

        [Display(Name = "select a Cinema")]
        [Required(ErrorMessage = "Movie Cinema Is Required")]

        public int CinemaId { get; set; }

        [Display(Name = "select a produce")]
        [Required(ErrorMessage = "Movie produce Is Required")]

        public int producerId { get; set; }



    }
}
