using BusinessLayer.Bl.Interfaces;
using BusinessLayer.Data;
using Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Bl.Classes
{
    public class Sliders : ISliders
    {
        private readonly LapShopContext _context;
        public Sliders(LapShopContext context)
        {
            _context = context;
        }
        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        var image = _context.TbSliders.FirstOrDefault(i => i.SliderId == id);
        //        if (image != null)
        //        {
        //            _context.TbSliders.Remove(image);
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error retrieving Slider: " + ex.Message);
        //    }
        //}

        public List<TbSlider> GetAll()
        {
            try
            {
                return (_context.TbSliders.ToList());
            }
            catch (Exception ex) 
            {
                throw new Exception("Error retrieving Slider: " + ex.Message);
            }
        }

        public TbSlider GetById(int id)
        {
            try
            {
                return (_context.TbSliders.FirstOrDefault(i => i.SliderId == id));
            }
            catch (Exception ex) 
            {
                throw new Exception("Error retrieving Slider: " + ex.Message);
            }


        }

        public bool Save(TbSlider slider)
        {
            try
            {
                if (slider != null)
                {
                    _context.Add(slider);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Slider: " + ex.Message);
            }
        }

        public void Delete(int id)
        {
            var slider = _context.TbSliders
                                 .FirstOrDefault(x => x.SliderId == id);

            if (slider == null)
                return;

            _context.TbSliders.Remove(slider);
            _context.SaveChanges();
        }
        public void Update(TbSlider slider)
        {
            try
            {
                if (slider == null)
                {
                    throw new ArgumentNullException(nameof(slider));
                }
                _context.TbSliders.Update(slider);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating Slider: " + ex.Message);
            }
           
        }
    }
}
