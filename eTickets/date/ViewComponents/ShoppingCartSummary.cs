using eTickets.date.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.date.ViewComponents
{
    [ViewComponent]
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View("Defualt", items.Count);
        }
    }
}
