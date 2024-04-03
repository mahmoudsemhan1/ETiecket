using eTickets.date.Cart;
using eTickets.date.Services;
using eTickets.date.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesServices _moviesServices;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesServices moviesServices, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesServices = moviesServices;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }
        public async Task<IActionResult> Index()
        {
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserRole = User.FindFirstValue(ClaimTypes.Role);
            var Orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(UserId, UserRole);
            return View(Orders);

        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                shoppingCart = _shoppingCart,
                shoppingcartTotal = _shoppingCart.GetShoppingCartTotal()

            };
            return View("ShoppingCart", response);
        }
        public async Task<RedirectToActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesServices.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesServices.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, UserId, UserEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View();

        }


    }
}
