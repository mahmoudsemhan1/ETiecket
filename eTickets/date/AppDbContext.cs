using eTickets.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eTickets.date
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.MovieId,
                am.ActorId
            });
            ;
           

                modelBuilder.Entity<Actor_Movie>().HasOne(m => m.movie).WithMany(am => am.Actor_Movies).HasForeignKey(am => am.MovieId);
                modelBuilder.Entity<Actor_Movie>().HasOne(m => m.actor).WithMany(am => am.Actor_Movies).HasForeignKey(am => am.ActorId);



                base.OnModelCreating(modelBuilder);
            }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actor_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }

        //Orders Related Tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<ShoppingCartItem>  shoppingCartItems  { get; set; }
    }
}
