using Domains.Models;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Data;
using BusinessLayer.Bl.Interfaces;

namespace BusinessLayer.Bl.Classes
{
    public class Categories : ICategories
    {
        private readonly LapShopContext _context;
        public Categories(LapShopContext context)
        {
            _context = context;
        }
        public List<TbCategory> GetAllCategories()
        {
            try
            {
                List<TbCategory> categories = _context.TbCategories.Where(c => c.CurrentState == 1).ToList();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving categories: " + ex.Message);
            }
        }
        public TbCategory GetCategoryById(int categoryId)
        {
            try
            {
                var category = _context.TbCategories.FirstOrDefault(c => c.CategoryId == categoryId && c.CurrentState == 1);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving category: " + ex.Message);
            }
        }
        public bool Save(TbCategory category)
        {
            try
            {
                
                if (category.CategoryId == 0)
                {
                    category.CreatedBy = "1";
                    category.CreatedDate = DateTime.Now;
                    var model = _context.TbCategories.Add(category);

                }
                else
                {
                    //var cat = context.TbCategories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                    category.UpdatedBy = "1";
                    category.UpdatedDate = DateTime.Now;
                    _context.Entry(category).State = Microsoft.
                        EntityFrameworkCore.EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving category: " + ex.Message);


            }
        }
        public bool Delete(int categoryId)
        {
            try
            {
                var category = GetCategoryById(categoryId);
                if (category != null)
                {
                    category.CurrentState = 0;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("Category not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting category: " + ex.Message);
            }
        }
    }
}
