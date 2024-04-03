using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.date.Services
{
    public class OrdersService : IOrdersService
    {
        public readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId,string UserRole)
        {
            var orders = await _context.Orders.Include(n=>n.orderItems).ThenInclude(n=>n.Movie).Include(n=>n.User).ToListAsync();
            if (UserRole != "Admin")
            {
                orders =  _context.Orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string UserId, string UserEmailAddress)
        {
            var order = new Order()
            {
                UserId = UserId,
                Email = UserEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach(var item in items) {
                var orderitem = new OrderItem
                {
                    Amount = item.Amount,
                    MovieId=item.Movie.Id,
                    OrdeId=order.Id,
                    Price=item.Movie.Price
                };
                await _context.orderItems.AddAsync(orderitem);         
            }
            await _context.SaveChangesAsync();

        }
    }
}
