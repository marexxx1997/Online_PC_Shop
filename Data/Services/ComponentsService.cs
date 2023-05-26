using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Base;
using OnlineShop.Data.ViewModels;
using OnlineShop.Models;
namespace OnlineShop.Data.Services
{
    public class ComponentsService:EntityBaseRepository<Components>,IComponentsService
    {
        private readonly AppDbCContext _context;
        public ComponentsService(AppDbCContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddNewComponentAsync(NewComponentVM data)
        {
            var newComponent = new Components()
            {
                Manufacturer = data.Manufacturer,
                Model = data.Model,
                Description = data.Description,
                Price = data.Price,
                Quantity = data.Quantity,
                Warranty = data.Warranty,
                ComponentsTypesId = data.ComponentsTypesId,
                ComponentsPictureUrl = data.ComponentsPictureUrl
            };
            await _context.Components.AddAsync(newComponent);
            await _context.SaveChangesAsync();

        }
        public async Task<Components> GetComponentByIdAsync(int id)
        {
            var componentDetails = _context.Components.FirstOrDefaultAsync(n => n.Id == id);
            return await componentDetails;
        }

        public async Task UpdateComponentAsync(NewComponentVM data)
        {
            var dbComponent = await _context.Components.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbComponent != null)
            {


                dbComponent.Manufacturer = data.Manufacturer;
                dbComponent.Model = data.Model;
                dbComponent.Description = data.Description;
                dbComponent.Price = data.Price;
                dbComponent.Quantity = data.Quantity;
                dbComponent.Warranty = data.Warranty;
                dbComponent.ComponentsTypesId = data.ComponentsTypesId;
                dbComponent.ComponentsPictureUrl = data.ComponentsPictureUrl;

                await _context.SaveChangesAsync();
            }

        }
        public async Task<List<string>> GetManufacturersAsync(int componentTypesId)
        {
            var components = await _context.Components.Where(x => x.ComponentsTypesId == componentTypesId).ToListAsync();
            List<Components> manufacturersList = new List<Components>();
            var manufacturers = components.Select(x => x.Manufacturer).Distinct().ToList();
            return  manufacturers;
        }
    }
}
