using eTickets.date.Base;
using eTickets.date.ViewModels;
using eTickets.Models;

namespace eTickets.date.Services
{
    public interface IMoviesServices:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownvalue();
        Task AddNewMovieAsync(NewMovieMV data);
        Task UpdateMovieAsync(NewMovieMV data);


    }
}
