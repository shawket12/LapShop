using Domains.Models;

namespace BusinessLayer.Bl.Interfaces
{
    public interface Iitems
    {
        public List<VwItem> GetAllItems(int? categoryId, int? itemTypeId);
        public TbItem GetItemById(int itemId);
        public List<VwItem> GetRecommendedItems(int itemId);
        public VwItem GetItemId(int itemId);
        public bool Save(TbItem item);
        public bool Delete(int itemId);
    }
}
