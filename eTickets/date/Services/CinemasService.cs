using eTickets.date.Base;
using eTickets.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace eTickets.date.Services
{
    public class ActorsService :EntityBaseRepository<Actor>, IActionsService
    {
        private readonly AppDbContext _context;

        public ActorsService(AppDbContext context):base(context){    }
    }
}
