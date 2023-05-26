using OnlineShop.Data.Base;
using OnlineShop.Data.ViewModels;
using OnlineShop.Models;
namespace OnlineShop.Data.Services
{
    public interface IComponentsService : IEntityBaseRepository<Components>
    {
        Task<Components> GetComponentByIdAsync(int id);
        Task AddNewComponentAsync(NewComponentVM data);
        Task UpdateComponentAsync(NewComponentVM data);

        Task<List<string>> GetManufacturersAsync(int componentTypesId);
    }
}
