using OnlineShop.Models;

namespace OnlineShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItemNew> items, string userId, string userEmailAddress);
        Task<List<Order>> GerOrdersByUserIdAndRoleAsync(string userId, string userRole);

    }
}
