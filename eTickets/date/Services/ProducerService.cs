using eTickets.date.Base;
using eTickets.Models;

namespace eTickets.date.Services
{
    public class ProducerService:EntityBaseRepository<Producer>,IProducerService
    {
        public ProducerService( AppDbContext context):base(context)
        {
            
        }

    }
}
