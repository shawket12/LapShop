using Domains.Models;

namespace BusinessLayer.Bl.Interfaces
{
    public interface IitemImage
    {
       
        public List<TbItemImage> GetImageByItemId(int itemId);
       
    }
}
