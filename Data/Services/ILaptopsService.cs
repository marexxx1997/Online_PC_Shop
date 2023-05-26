using OnlineShop.Data.Base;
using OnlineShop.Models;

namespace OnlineShop.Data.Services
{
    public interface ILaptopsService:IEntityBaseRepository<Laptop>
    {
        Task<Laptop> GetLaptopByIdAsync(int id);
        Task AddNewLaptopAsync(NewLaptopVM data);
        Task UpdateLaptopAsync(NewLaptopVM data);

    }
}
