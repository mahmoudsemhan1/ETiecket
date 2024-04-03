using eTickets.Models;

namespace eTickets.date.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            producers = new List<Producer>();
            cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }
        public List<Producer> producers { get; set; }
        public List<Cinema> cinemas { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
