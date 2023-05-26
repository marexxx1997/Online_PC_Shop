using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbCContext _context { get; set; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItemNew> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbCContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbCContext>();
            string cartId=session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId=cartId};

        }

        public void AddItemToCart(Laptop laptop, Components component)
        {
            if(laptop != null)
            {
                var shoppingCartItem = _context.ShoppingCartItemsNew.FirstOrDefault(n => n.Laptop.Id == laptop.Id && n.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItemNew()
                    {
                        ShoppingCartId = ShoppingCartId,
                        Laptop = laptop,
                        Component = null,
                        Amount = 1
                    };

                    _context.ShoppingCartItemsNew.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                }

            }
            else
            {
                var shoppingCartItem = _context.ShoppingCartItemsNew.FirstOrDefault(n => n.Component.Id == component.Id && n.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItemNew()
                    {
                        ShoppingCartId = ShoppingCartId,
                        Laptop = null,
                        Component = component,
                        Amount = 1
                    };

                    _context.ShoppingCartItemsNew.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                }
            }

            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Laptop laptop, Components component)
        {
            if(laptop!=null)
            { 
            var shoppingCartItem = _context.ShoppingCartItemsNew.FirstOrDefault(n => n.Laptop.Id == laptop.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItemsNew.Remove(shoppingCartItem);
                }

                
            }
            }
            else
            {
                var shoppingCartItem = _context.ShoppingCartItemsNew.FirstOrDefault(n => n.Component.Id == component.Id && n.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem != null)
                {
                    if (shoppingCartItem.Amount > 1)
                    {
                        shoppingCartItem.Amount--;
                    }
                    else
                    {
                        _context.ShoppingCartItemsNew.Remove(shoppingCartItem);
                    }


                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItemNew> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems=_context.ShoppingCartItemsNew.Where(n=> n.ShoppingCartId== ShoppingCartId).Include(n=>n.Laptop)
                .Include(n=>n.Component).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total_laptops = _context.ShoppingCartItemsNew.Where(x=>x.Laptop!=null).Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Laptop.Price * n.Amount).Sum();
            var total_components = _context.ShoppingCartItemsNew.Where(x => x.Component != null).Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Component.Price * n.Amount).Sum();
            return (total_components + total_laptops);
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItemsNew.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItemsNew.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
