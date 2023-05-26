using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Data.Services;
using OnlineShop.Models;
using System.Linq;
using OnlineShop.Data.Enums;
using PagedList;



namespace OnlineShop.Controllers
{
    public class LaptopsController : Controller
    {
        //private readonly AppDbCContext _context;
        private readonly ILaptopsService _service;

        public LaptopsController(ILaptopsService service)
        {
            _service = service;
        }

        //public async Task<IActionResult> Index(int page = 1, int pageSize = 9)
        //{
        //    var data = await _service.GetAllAsync();
        //    var pagedData = data.ToPagedList(page, pageSize);
        //    return View(pagedData);
        //}
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            var laptopdata = data.Where(x => x.LaptopsTypesId == 3);
            return View(laptopdata);
        }
        public async Task<IActionResult> GetAllPC()
        {
            //var data = await _service.GetAllAsync();
            var data = await _service.GetAllAsync();
            var pcdata = data.Where(x => x.LaptopsTypesId == 4);
            return View("Index",pcdata);
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allLaptops = await _service.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allLaptops.Where(n => n.Model.ToUpper().Contains(searchString.ToUpper()) || n.Description.ToUpper().Contains(searchString.ToUpper())).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", allLaptops);
        }

        
        public async Task<IActionResult> FilterByBrand(Manufacturer Manufacturer, Double minPrice, Double maxPrice, int LaptopsTypesId)
        {

            var laptops = await _service.GetAllAsync();
            var allLaptops = laptops.Where(l => l.LaptopsTypesId == LaptopsTypesId).ToList();
            int nullCheck = Convert.ToInt32(Manufacturer);
            if (nullCheck == 0)
            {
                var reslults = allLaptops.Where(n => n.Price >= minPrice && n.Price <= maxPrice).ToList();
                return View("Index", reslults);
            }
       

            var filteredResults = allLaptops.Where(n => n.Manufacturer == Manufacturer && n.Price>=minPrice && n.Price<=maxPrice).ToList();
            return View("Index", filteredResults);

        }
        public async Task<IActionResult> FilterByPrice(Manufacturer Manufacturer, int id, int LaptopsTypesId)
        {
            var laptops=await _service.GetAllAsync();
            var allLaptops = laptops.Where(l => l.LaptopsTypesId == LaptopsTypesId).ToList();
            int nullCheck = Convert.ToInt32(Manufacturer);

            if (nullCheck == 0)
            {
                if (id == 0)
                {
                    
                    return View("Index", allLaptops);
                }
                else if (id == 1)
                {
                    var filteredResults = allLaptops.Where(n => n.Price >= 500 && n.Price <= 1000).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 2)
                {
                    var filteredResults = allLaptops.Where(n => n.Price >= 1000 && n.Price <= 1500).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 3)
                {
                    var filteredResults = allLaptops.Where(n => n.Price >= 1500 && n.Price <= 2000).ToList();
                    return View("Index", filteredResults);
                }
                else
                {
                    var filteredResults = allLaptops.Where(n => n.Price >= 2000).ToList();
                    return View("Index", filteredResults);
                }
            }
            
                if (id == 0)
                {
                    var filteredResults = allLaptops.Where(n => n.Manufacturer == Manufacturer);
                    return View("Index", filteredResults);
                }
                else if (id == 1)
                {
                    var filteredResults = allLaptops.Where(n => n.Manufacturer == Manufacturer && n.Price >= 500 && n.Price <= 1000).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 2)
                {
                    var filteredResults = allLaptops.Where(n => n.Manufacturer == Manufacturer && n.Price >= 1000 && n.Price <= 1500).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 3)
                {
                    var filteredResults = allLaptops.Where(n => n.Manufacturer == Manufacturer && n.Price >= 1500 && n.Price <= 2000).ToList();
                    return View("Index", filteredResults);
                }
                else
                {
                    var filteredResults = allLaptops.Where(n => n.Manufacturer == Manufacturer && n.Price >= 2000).ToList();
                    return View("Index", filteredResults);
                }
            
        }
        public async Task<IActionResult> Sort(Manufacturer Manufacturer, int id, int LaptopsTypesId)
        {
            var laptops = await _service.GetAllAsync();
            var allLaptops = laptops.Where(l => l.LaptopsTypesId == LaptopsTypesId).ToList();
            int nullCheck = Convert.ToInt32(Manufacturer);
            if (nullCheck == 0)
            {
                if(id==5)
                {
                    var reslults = allLaptops.OrderBy(n=>n.Price).ToList();
                    return View("Index", reslults);
                }
                var descendingResults = allLaptops.OrderByDescending(n => n.Price).ToList();
                return View("Index", descendingResults);

            }
            else
            {
                if (id == 5)
                {
                    var ascendingResults = allLaptops.Where(n => n.Manufacturer == Manufacturer).OrderBy(p => p.Price).ToList();
                    return View("Index", ascendingResults);
                }

                var descendingResults = allLaptops.Where(n => n.Manufacturer == Manufacturer).OrderByDescending(p=>p.Price).ToList();
                return View("Index", descendingResults);
            }
            
        }

        public async Task<IActionResult> Details(int id)
        {
            var laptopDetail = await _service.GetLaptopByIdAsync(id);
            return View(laptopDetail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewLaptopVM laptop)
        {
            if(!ModelState.IsValid)
            {
                return View(laptop);    
            }
            await _service.AddNewLaptopAsync(laptop);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var laptopDetails = await _service.GetLaptopByIdAsync(id);
            if (laptopDetails == null) return View("NotFound");
            var response = new NewLaptopVM()
            {
                Id = laptopDetails.Id,
                Model = laptopDetails.Model,
                Manufacturer = laptopDetails.Manufacturer,
                RAM = laptopDetails.RAM,
                Price = laptopDetails.Price,
                Processor = laptopDetails.Processor,
                ProfilePictureUrl = laptopDetails.ProfilePictureUrl,
                OS = laptopDetails.OS,
                Description = laptopDetails.Description,
                ScreenSize = laptopDetails.ScreenSize,
                GraphicCard = laptopDetails.GraphicCard,
                HardDisc = laptopDetails.HardDisc,
                Quantity = laptopDetails.Quantity,
                Warranty = laptopDetails.Warranty,
                LaptopsTypesId= laptopDetails.LaptopsTypesId

            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewLaptopVM laptop)
        {
            if (id != laptop.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(laptop);
            }
            await _service.UpdateLaptopAsync(laptop);
            return RedirectToAction(nameof(Index));
        }
    }
}
