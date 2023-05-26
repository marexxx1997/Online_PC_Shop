using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;

namespace OnlineShop.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbCContext _context;
        public OrdersService(AppDbCContext context)
        {
            _context = context;
        }
    
        public async Task<List<Order>> GerOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {

            var orders = await _context.Orders
                .Include(n => n.OrderItemsNew).ThenInclude(n => n.Laptop)
                .Include(n => n.OrderItemsNew).ThenInclude(n => n.Component)
                .Include(n => n.User)
                .ToListAsync();
            //var orders = await _context.Orders.ToListAsync();
            //var orders = await _context.Orders.Include(n => n.OrderItemsNew).ThenInclude(n => n.Laptop).Include(n => n.User).ToListAsync();
            //foreach (var order in orders)
            //{
            //    foreach (var item in order.OrderItemsNew)
            //    {
            //        item.Laptop = item.Laptop ?? new Laptop();
            //        item.Component = item.Component ?? new Components();
            //    }
            //}
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItemNew> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItemNew()
                {
                    Amount = item.Amount,
                    LaptopId = item.Laptop?.Id,
                    ComponentId=item.Component?.Id,
                    OrderId = order.Id,
                    Price = item.Laptop != null ? item.Laptop.Price : item.Component.Price
                };
                await _context.OrderItemsNew.AddAsync(orderItem);
                if(item.Laptop!=null)
                {
                    var laptopProduct = await _context.Laptops.FirstOrDefaultAsync(x => x.Id == item.Laptop.Id);
                    laptopProduct.Quantity -= item.Amount;
                }
                if(item.Component!=null)
                {
                    var componentProduct = await _context.Components.FirstOrDefaultAsync(x => x.Id == item.Component.Id);
                    componentProduct.Quantity -= item.Amount;
                }

            }
            await _context.SaveChangesAsync();
        }
    }
}
