using Azure;
using eTickets.date.Base;
using eTickets.date.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace eTickets.date.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesServices
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMovieAsync(NewMovieMV data)
        {
            var NewMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                producerId = data.producerId
            };
            await _context.Movies.AddAsync(NewMovie);
            await _context.SaveChangesAsync();

            //add movie Actors
            foreach (var ActorId in data.ActorIds)
            {
                var NewActorMovie = new Actor_Movie()
                {
                    MovieId= NewMovie.Id,
                    ActorId= ActorId

                };
                await _context.Actor_Movies.AddAsync(NewActorMovie);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var MovieDetails = await _context.Movies
                 .Include(c => c.Cinema)
                 .Include(p => p.producer)
                 .Include(am => am.Actor_Movies).ThenInclude(a => a.actor)
                 .FirstOrDefaultAsync(n => n.Id == id);

            return MovieDetails;



        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownvalue()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
                cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync()
            };

            return response;
        }

        public async Task UpdateMovieAsync(NewMovieMV data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbMovie != null)
            {

                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                   dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.producerId = data.producerId;
                await _context.SaveChangesAsync();

          
               
            }
            //remove existing actors
            var existingactorsDb = _context.Actor_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actor_Movies.RemoveRange(existingactorsDb);
            await _context.SaveChangesAsync();


            //add movie Actors
            foreach (var ActorId in data.ActorIds)
            {
                var NewActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = ActorId

                };
                await _context.Actor_Movies.AddAsync(NewActorMovie);
            }
            await _context.SaveChangesAsync();

        }
    }
}
