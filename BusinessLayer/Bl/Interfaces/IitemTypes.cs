using Domains.Models;

namespace BusinessLayer.Bl.Interfaces
{
    public interface IitemTypes
    {
        public List<TbItemType> GetAllItemTypes();
        public TbItemType GetItemTypeById(int itemTypeId);
        public bool Save(TbItemType itemTypeId);
        public bool Delete(int itemTypeId);
    }
}
