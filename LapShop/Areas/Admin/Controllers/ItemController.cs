using BusinessLayer.Bl.Classes;
using BusinessLayer.Bl.Interfaces;
using Domains.Models;
using LapShop.Utlities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace LapShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Data Entry")]
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly Iitems _items;
        private readonly IitemTypes _iitemTypes;  
        private readonly ICategories _categories;
        private readonly IOs _os;
        public ItemController(Iitems items, ICategories categories,IitemTypes iitemTypes,IOs os )
        {
            _items = items;
            _categories = categories;
            _iitemTypes = iitemTypes;
            _os = os;
        }
        [AllowAnonymous]
        public IActionResult List()
        {
            ViewBag.Categories = _categories.GetAllCategories();
            ViewBag.Types = _items.GetAllItems(null, null).Select(i => new
            {
                i.ItemTypeId,
                i.ItemTypeName
            }).Distinct().ToList();
            //ViewBag.Types = _iitemTypes.GetAllItemTypes();
            var items = _items.GetAllItems(null,null);
            return View(items);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? itemId)
        {
            ViewBag.Categories= _categories.GetAllCategories();
            ViewBag.Types= _iitemTypes.GetAllItemTypes();
            ViewBag.OS= _os.GetAllOS();
            
            var item = new TbItem();
            if (itemId != null)
            {
                 item = _items.GetItemById(itemId.Value);
            }
            return View(item);
        }
        public IActionResult Search(int categoryId, int itemTypeId) 
        {
            ViewBag.Categories = _categories.GetAllCategories();
            ViewBag.Types = _items.GetAllItems(null,null).Select(i => new
            {
                i.ItemTypeId,
                i.ItemTypeName
            }).Distinct().ToList();
            var items = _items.GetAllItems(categoryId, itemTypeId);
            return View("List", items);
        }
        public async Task<IActionResult> Save(TbItem item, IFormFile? files)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", item);
            }
            // لو المستخدم اختار صورة جديدة
            if (files != null && files.Length > 0)
            {
                // احذف الصورة القديمة
                Helper.DeleteImage(item.ImageName, "items");

                // ارفع الجديدة
                item.ImageName =
                    await Helper.UploadImage(files, "Items");
            }
            item.CurrentState = 1;
            _items.Save(item);
            return RedirectToAction("List");
        }
        public IActionResult Delete(int itemId)
        {
            _items.Delete(itemId);
            return RedirectToAction("List");
        }

    }
}
