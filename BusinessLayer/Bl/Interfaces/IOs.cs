using Domains.Models;

namespace BusinessLayer.Bl.Interfaces
{
    public interface IOs
    {
        public List<TbO> GetAllOS();
        public TbO GetOsById(int osId);
        public bool Save(TbO os);
        public bool Delete(int osId);
    }
}
