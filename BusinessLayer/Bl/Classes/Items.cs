using BusinessLayer.Bl.Interfaces;
using BusinessLayer.Data;
using Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Bl.Classes
{
        public class Items : Iitems
        {
            private readonly LapShopContext _context;
            public Items(LapShopContext context)
            {
                _context = context;
            }
            public List<VwItem> GetAllItems(int? categoryId, int? itemTypeId)
            {
                try
                {
                    List<VwItem> items = _context.VwItems.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0) && (a.ItemTypeId == itemTypeId || itemTypeId == null || itemTypeId == 0)&& (a.CurrentState == 1))
                    .ToList();
                    return items;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving items: " + ex.Message);
                }
            }
       
        public TbItem GetItemById(int id)
        {
            try
            {
                var item = _context.TbItems.FirstOrDefault(a => a.ItemId == id && a.CurrentState == 1);
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving items: " + ex.Message);
            }
        }
        public List<VwItem> GetRecommendedItems(int itemId)
        {
            try
            {
                var item = _context.TbItems.FirstOrDefault(a => a.ItemId == itemId && a.CurrentState == 1);

                var recommendedItems = _context.VwItems.Where(a=>a.SalesPrice>=item.SalesPrice-50 && a.SalesPrice<=item.SalesPrice+50
                && a.CurrentState == 1).OrderByDescending(a => a.CreatedDate).ToList();
                return recommendedItems;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving items: " + ex.Message);
            }
        }

        public VwItem GetItemId(int itemId)
        {
            try
            {
                var item = _context.VwItems.FirstOrDefault(a => a.ItemId == itemId && a.CurrentState == 1);
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving items: " + ex.Message);
            }
        }
        public bool Save(TbItem Item)
        {
            try
            {
                Item.CurrentState = 1;

                if (Item.ItemId == 0)
                {
                    
                    Item.CreatedBy = "1";
                    Item.CreatedDate = DateTime.Now;
                    var model = _context.TbItems.Add(Item);

                }
                else
                {
                    //var cat = context.TbItems.FirstOrDefault(c => c.ItemId == Item.ItemId);
                    Item.UpdatedBy = "1";
                    Item.UpdatedDate = DateTime.Now;
                    _context.Entry(Item).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Item: " + ex.Message);


            }
        }
        public bool Delete(int ItemId)
        {
            try
            {
                var Item = GetItemById(ItemId);
                if (Item != null)
                {
                    Item.CurrentState = 0;
                    _context.Entry(Item).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception("Item not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting Item: " + ex.Message);
            }
        }


    }

}
