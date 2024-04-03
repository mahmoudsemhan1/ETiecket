using eTickets.date.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.date.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShopingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShopingCartId = cartId };
        }
        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShopingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShopingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _context.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShopingCartId).Include(n => n.Movie).ToList());
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var ShoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShopingCartId);
            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    _context.shoppingCartItems.Remove(ShoppingCartItem);
                }


            }

            _context.SaveChanges();
        }
        public double GetShoppingCartTotal()
        {
            var total = _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShopingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;

        }
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.shoppingCartItems.Where(n => n.ShoppingCartId == ShopingCartId).ToListAsync();
            _context.shoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
