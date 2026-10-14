using BusinessLayer.Bl.Interfaces;
using Domains.Models;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly Iitems _iitems;
        private readonly ISliders _sliders;
        public HomeController(Iitems iitems, ISliders sliders)
        {
            _iitems = iitems;
            _sliders = sliders;
        }
        public IActionResult Index()
        {
            var vm = new VmHomePage();
            vm.LstSliders = _sliders.GetAll().ToList();
            var allItems = _iitems.GetAllItems(null, null);
            vm.LstNewItems = allItems.OrderByDescending(i => i.CreatedDate).Take(8).ToList();
            vm.LstAllItems = allItems.Take(24).ToList();
            

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
