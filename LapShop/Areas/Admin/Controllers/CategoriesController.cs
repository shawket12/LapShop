using AutoMapper;
using BusinessLayer.Bl.Classes;
using BusinessLayer.Bl.Interfaces;
using Domains.Models;
using LapShop.Utlities;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // حقن IUnitOfWork عبر الـ Constructor Injection
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> List()
        {
            // جلب كل الفئات عبر UnitOfWork بشكل Async
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoriesVm = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(categoriesVm);
        }
        public async Task<IActionResult> Edit(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
            {
                // إضافة فئة جديدة
                return View(new CategoryViewModel());
            }

            // جلب الفئة للتعديل
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId.Value);
            if (category == null)
            {
                return NotFound();
            }
            var categoryVm = _mapper.Map<CategoryViewModel>(category);
            return View(categoryVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, IFormFile? Files)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", category);
            }

            if (category.CategoryId == 0)
            {
                // إضافة Category جديدة

                if (Files != null && Files.Length > 0)
                {
                    category.ImageName = await Helper.UploadImage(Files, "Categories");
                }

                await _unitOfWork.Categories.AddAsync(category);
            }
            else
            {
                // تعديل Category موجودة

                var existingCategory =
                    await _unitOfWork.Categories.GetByIdAsync(category.CategoryId);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                // لو المستخدم اختار صورة جديدة
                if (Files != null && Files.Length > 0)
                {
                    // احذف الصورة القديمة
                    Helper.DeleteImage(existingCategory.ImageName, "Categories");

                    // ارفع الجديدة
                    existingCategory.ImageName =
                        await Helper.UploadImage(Files, "Categories");
                }

                // تحديث باقي البيانات
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.ShowInHomePage = category.ShowInHomePage;

                _unitOfWork.Categories.Update(existingCategory);
            }

            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category != null)
            {
                _unitOfWork.Categories.Delete(category);
                await _unitOfWork.CompleteAsync();
            }

            return RedirectToAction(nameof(List));
        }


    }
}
