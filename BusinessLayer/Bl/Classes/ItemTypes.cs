using Domains.Models;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Data;
using BusinessLayer.Bl.Interfaces;

namespace BusinessLayer.Bl.Classes
{
    public class ItemTypes : IitemTypes
    {
        private readonly LapShopContext _context;
        public ItemTypes(LapShopContext context)
        {
            _context = context;
        }
        public List<TbItemType> GetAllItemTypes()
        {
            try
            {
                List<TbItemType> tbItemTypes = _context.TbItemTypes.Where(it => it.CurrentState == 1).ToList();
                return tbItemTypes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving TbItemType: " + ex.Message);
            }
        }
        public TbItemType GetItemTypeById(int tbItemTypeId)
        {
            try
            {
                var tbItemType = _context.TbItemTypes.FirstOrDefault(c => c.ItemTypeId == tbItemTypeId && c.CurrentState == 1);
                return tbItemType;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving tbItemType: " + ex.Message);
            }
        }
        public bool Save(TbItemType tbItemType)
        {
            try
            {
                
                if (tbItemType.ItemTypeId == 0)
                {
                    tbItemType.CreatedBy = "1";
                    tbItemType.CreatedDate = DateTime.Now;
                    var model = _context.TbItemTypes.Add(tbItemType);

                }
                else
                {
                    //var cat = context.TbTbItemType.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                    tbItemType.UpdatedBy = "1";
                    tbItemType.UpdatedDate = DateTime.Now;
                    _context.Entry(tbItemType).State =EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving tbItemType: " + ex.Message);


            }
        }
        public bool Delete(int tbItemType)
        {
            try
            {
                var ItemType = GetItemTypeById(tbItemType);
                if (ItemType != null)
                {
                    ItemType.CurrentState = 0;
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
