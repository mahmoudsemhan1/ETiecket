 using eTickets.Models;

namespace eTickets.date.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem>items ,string UserId,string UserEmailAddress);
        Task <List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string UserRole);

    }
}
