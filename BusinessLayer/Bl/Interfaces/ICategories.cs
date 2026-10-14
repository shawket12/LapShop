using Domains.Models;

namespace BusinessLayer.Bl.Interfaces
{
    public interface ICategories
    {
        public List<TbCategory> GetAllCategories();
        public TbCategory GetCategoryById(int categoryId);
        public bool Save(TbCategory category);
        public bool Delete(int categoryId);
    }
}
