using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Base;
using OnlineShop.Models;

namespace OnlineShop.Data.Services
{
    public class LaptopsService:EntityBaseRepository<Laptop>,ILaptopsService
    {
        private readonly AppDbCContext _context;
        public LaptopsService(AppDbCContext context):base(context)
        {
            _context = context; 
        }

        public async Task AddNewLaptopAsync(NewLaptopVM data)
        {
            var newLaptop = new Laptop()
            {
                Manufacturer = data.Manufacturer,
                Model = data.Model,
                RAM = data.RAM,
                Description = data.Description,
                GraphicCard = data.GraphicCard,
                HardDisc = data.HardDisc,
                OS = data.OS,
                Price = data.Price,
                Processor = data.Processor,
                ProfilePictureUrl = data.ProfilePictureUrl,
                Quantity = data.Quantity,
                ScreenSize = data.ScreenSize,
                Warranty = data.Warranty,
                LaptopsTypesId = data.LaptopsTypesId
            };
            await _context.Laptops.AddAsync(newLaptop);
            await _context.SaveChangesAsync();

        }

        public async Task<Laptop> GetLaptopByIdAsync(int id)
        {
            var laptopDetails=_context.Laptops.FirstOrDefaultAsync(n => n.Id == id);
            return await laptopDetails;
        }

        public async Task UpdateLaptopAsync(NewLaptopVM data)
        {
            var dbLaptop = await _context.Laptops.FirstOrDefaultAsync(n => n.Id == data.Id);
            if(dbLaptop != null)
            {


                dbLaptop.Manufacturer = data.Manufacturer;
                dbLaptop.Model = data.Model;
                dbLaptop.RAM = data.RAM;
                dbLaptop.Description = data.Description;
                dbLaptop.GraphicCard = data.GraphicCard;
                dbLaptop.HardDisc = data.HardDisc;
                dbLaptop.OS = data.OS;
                dbLaptop.Price = data.Price;
                dbLaptop.Processor = data.Processor;
                dbLaptop.ProfilePictureUrl = data.ProfilePictureUrl;
                dbLaptop.Quantity = data.Quantity;
                dbLaptop.ScreenSize = data.ScreenSize;
                dbLaptop.Warranty = data.Warranty;
                dbLaptop.LaptopsTypesId=data.LaptopsTypesId;
                
                await _context.SaveChangesAsync();
            }
                
        }
    }
}
