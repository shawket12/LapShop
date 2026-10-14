using BusinessLayer.Bl.Interfaces;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        private readonly Iitems _items;
        private readonly ICategories _categories;
        private readonly ISliders _sliders;

        public HomeController(Iitems items, ICategories categories, ISliders sliders)
        {
            _items = items;
            _categories = categories;
            _sliders = sliders;
        }

        [Authorize(Roles = ("Admin"))]
        public IActionResult Index()
        {
            var allItems = _items.GetAllItems(null, null);

            var vm = new VmAdminDashboard
            {
                ItemsCount = allItems.Count,
                CategoriesCount = _categories.GetAllCategories().Count,
                SlidersCount = _sliders.GetAll().Count,
                LatestItems = allItems.OrderByDescending(i => i.CreatedDate).Take(5).ToList()
            };

            return View(vm);
        }
    }
}
