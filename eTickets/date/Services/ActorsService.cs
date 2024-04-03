using eTickets.date.Base;
using eTickets.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace eTickets.date.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService 
    {
        private readonly AppDbContext _context;

        public CinemasService(AppDbContext context):base(context){    }
    }
}
