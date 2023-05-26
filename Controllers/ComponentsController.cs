using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Data.Services;
using OnlineShop.Data.ViewModels;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly IComponentsService _service;

        public ComponentsController(IComponentsService service)
        {
            _service = service;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var data = await _service.GetAllAsync();
        //    var laptopdata = data.Where(x => x.LaptopsTypesId == 3);
        //    return View(laptopdata);
        //}
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return View("Index",data);
        }
        public async Task<IActionResult> Index(int ComponentsTypesId)
        {
            var data = await _service.GetAllAsync();
            if (ComponentsTypesId==0)
            {
                return View("Index", data);
            }
            //var data=await _service.GetAllAsync();
            var components = data.Where(x => x.ComponentsTypesId == ComponentsTypesId);
            return View(components);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var componentDetails = await _service.GetComponentByIdAsync(id);
            if (componentDetails == null) return View("NotFound");
            var response = new NewComponentVM()
            {
                Id = componentDetails.Id,
                Model = componentDetails.Model,
                Manufacturer = componentDetails.Manufacturer,
                Price = componentDetails.Price,
                Description = componentDetails.Description,
                Quantity = componentDetails.Quantity,
                Warranty = componentDetails.Warranty,
                ComponentsTypesId = componentDetails.ComponentsTypesId,
                ComponentsPictureUrl=componentDetails.ComponentsPictureUrl

            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewComponentVM component)
        {
            if (id != component.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(component);
            }
            await _service.UpdateComponentAsync(component);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewComponentVM component)
        {
            if (!ModelState.IsValid)
            {
                return View(component);
            }
            await _service.AddNewComponentAsync(component);
            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> Details(int id)
        {
            var componentDetail = await _service.GetComponentByIdAsync(id);
            return View(componentDetail);
        }

        public async Task<IActionResult> GetManufacturers(int ComponentsTypesId)
        {
            var manufacturers = await _service.GetManufacturersAsync(ComponentsTypesId);
            ComponentsVM component = new ComponentsVM();
            component.ManufacturerList = new List<SelectListItem>();
            foreach(var manufacturer in manufacturers)
            {
                component.ManufacturerList.Add(
                    new SelectListItem
                    {
                        Value = manufacturer,
                        Text = manufacturer
                    }
                ) ;
            }
            return PartialView("_GetManufacturers",component);
        }
        public async Task<IActionResult> Sort( int id, int ComponentsTypesId)
        {
            var data = await _service.GetAllAsync();
            if (ComponentsTypesId > 0)
            {
                var components = data.Where(x => x.ComponentsTypesId == ComponentsTypesId);

                if (id == 5)
                {
                    var ascendingResults = components.OrderBy(p => p.Price).ToList();
                    return View("Index", ascendingResults);
                }

                var descendingResults = components.OrderByDescending(p => p.Price).ToList();
                return View("Index", descendingResults);
            }
            else
            {
                if (id == 5)
                {
                    var ascendingResults = data.OrderBy(p => p.Price).ToList();
                    return View("Index", ascendingResults);
                }

                var descendingResults = data.OrderByDescending(p => p.Price).ToList();
                return View("Index", descendingResults);
            }
        }
        public async Task<IActionResult> FilterByPrice(int id, int ComponentsTypesId)
        {
            var data = await _service.GetAllAsync();
            if (ComponentsTypesId > 0)
            {
                var components = data.Where(x => x.ComponentsTypesId == ComponentsTypesId);

                if (id == 0)
                {

                    return View("Index", components);
                }
                else if (id == 1)
                {
                    var filteredResults = components.Where(n => n.Price >= 0 && n.Price <= 100).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 2)
                {
                    var filteredResults = components.Where(n => n.Price >= 100 && n.Price <= 200).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 3)
                {
                    var filteredResults = components.Where(n => n.Price >= 200 && n.Price <= 500).ToList();
                    return View("Index", filteredResults);
                }
                else
                {
                    var filteredResults = components.Where(n => n.Price >= 500).ToList();
                    return View("Index", filteredResults);
                }
            }
            else
            {
                if (id == 0)
                {

                    return View("Index", data);
                }
                else if (id == 1)
                {
                    var filteredResults = data.Where(n => n.Price >= 0 && n.Price <= 100).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 2)
                {
                    var filteredResults = data.Where(n => n.Price >= 100 && n.Price <= 200).ToList();
                    return View("Index", filteredResults);
                }
                else if (id == 3)
                {
                    var filteredResults = data.Where(n => n.Price >= 200 && n.Price <= 500).ToList();
                    return View("Index", filteredResults);
                }
                else
                {
                    var filteredResults = data.Where(n => n.Price >= 500).ToList();
                    return View("Index", filteredResults);
                }
            }

        }

        public async Task<IActionResult> FilterByInputedPrice(Double minPrice, Double maxPrice, int ComponentsTypesId)
        {
            var data = await _service.GetAllAsync();
            if(ComponentsTypesId>0)
            {
                var components = data.Where(x => x.ComponentsTypesId == ComponentsTypesId);
                var reslults = components.Where(n => n.Price >= minPrice && n.Price <= maxPrice).ToList();
                return View("Index", reslults);
            }
            else
            {
                var reslults = data.Where(n => n.Price >= minPrice && n.Price <= maxPrice).ToList();
                return View("Index", reslults);
            }
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await _service.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = data.Where(n => n.Model.ToUpper().Contains(searchString.ToUpper()) || n.Description.ToUpper().Contains(searchString.ToUpper())).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", data);
        }
    }
}
