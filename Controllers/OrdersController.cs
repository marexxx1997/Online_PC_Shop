using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data.Cart;
using OnlineShop.Data.Services;
using OnlineShop.Data.ViewModels;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILaptopsService _laptopsService;
        private readonly IComponentsService _componentsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(ILaptopsService laptopsService, ShoppingCart shoppingCart, IOrdersService ordersService, IComponentsService componentsService)
        {
            _laptopsService = laptopsService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _componentsService = componentsService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders =await _ordersService.GerOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<RedirectToActionResult> AddToShoppingCart(int id, string type)
        {
            
            if (type == "laptop")
            {
                 var laptop = await _laptopsService.GetLaptopByIdAsync(id);
                if (laptop != null)
                {
                    _shoppingCart.AddItemToCart(laptop, null);
                }
            }
            else
            {
                 var component = await _componentsService.GetComponentByIdAsync(id);
                if (component != null)
                {
                    _shoppingCart.AddItemToCart(null, component);
                }
            }
            

            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id, string type)
        {
            if (type == "laptop")
            {
                var laptop = await _laptopsService.GetLaptopByIdAsync(id);
                if (laptop != null)
                {
                    _shoppingCart.RemoveItemFromCart(laptop, null);
                }
            }
            else
            {
                var component = await _componentsService.GetComponentByIdAsync(id);
                if (component != null)
                {
                    _shoppingCart.RemoveItemFromCart(null, component);
                }
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
