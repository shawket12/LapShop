using BusinessLayer.Bl.Interfaces;
using Domains.Models;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Controllers
{
    public class ItemsController : Controller
    {
        private readonly Iitems _iitems;
        private readonly IitemImage _iitemImage;
        public ItemsController(Iitems iitems, IitemImage iitemImage)
        {
            _iitems = iitems;
            _iitemImage = iitemImage;
        }
        public IActionResult ItemDetails(int id)
        {
           
            VmItemDetails vm=new VmItemDetails();
            vm.Item = _iitems.GetItemId(id); ;
            vm.LstItemImages = _iitemImage.GetImageByItemId(id);
            vm.LstRecommendedItems = _iitems.GetRecommendedItems(id).Take(15).ToList();

            return View(vm);
        }
    }
}
