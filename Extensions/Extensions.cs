using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Extensions
{
    public static class Extensions
    {
        public static List<string> GetManufacturers(this Components model, AppDbCContext context)
        {
            return context.Components
                .Select(x => x.Manufacturer)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

        }
    }
}
