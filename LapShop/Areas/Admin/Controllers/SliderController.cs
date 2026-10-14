using BusinessLayer.Bl.Classes;
using BusinessLayer.Bl.Interfaces;
using Domains.Models;
using LapShop.Utlities;
using LapShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace LapShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliders _sliders;
        public SliderController(ISliders sliders)
        {
            _sliders = sliders;
        }
        public IActionResult AddSlider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSlider(SliderViewModel slider, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View("AddSlider", slider);
            }

            TbSlider tbSlider = new TbSlider()
            {
                Title = slider.Title,
                Description = slider.Description,
                ImageName = await Helper.UploadImage(file, "Sliders")
            };

            _sliders.Save(tbSlider);

            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            var sliders = _sliders.GetAll();

            return View(sliders);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var slider = _sliders.GetById(id);

            if (slider == null)
                return NotFound();

            Helper.DeleteImage(slider.ImageName, "Sliders");

            _sliders.Delete(slider.SliderId);

            return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult UpdateSlider(int id)
        {
            var slider = _sliders.GetById(id);


                if (slider == null)
                return NotFound();

            SliderViewModel model = new SliderViewModel()
            {
                Title = slider.Title,
                Description = slider.Description,
                ImageName = slider.ImageName
            };

            ViewBag.SliderId = slider.SliderId;

            return View("UpdateSlider", model);


        }

      
       
[HttpPost]
public async Task<IActionResult> UpdateSlider(
    int id,
    SliderViewModel slider,
    IFormFile? file)
        {
            var existingSlider = _sliders.GetById(id);

            if (existingSlider == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.SliderId = id;

                slider ??= new SliderViewModel();

                slider.ImageName = existingSlider.ImageName;

                return View("UpdateSlider", slider);
            }

            existingSlider.Title = slider.Title;
            existingSlider.Description = slider.Description;

            if (file != null && file.Length > 0)
            {
                string oldImageName = existingSlider.ImageName;

                string newImageName =
                    await Helper.UploadImage(file, "Sliders");

                if (!string.IsNullOrEmpty(newImageName))
                {
                    existingSlider.ImageName = newImageName;

                    Helper.DeleteImage(oldImageName, "Sliders");
                }
            }

            _sliders.Update(existingSlider);

            return RedirectToAction("List");
        }





    }
}
