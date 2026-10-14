using Domains.Models;

namespace LapShop.ViewModels
{
    public class VmItemDetails
    {
        public VmItemDetails()
        {
            Item = new VwItem();
            LstItemImages = new List<TbItemImage>();
            LstRecommendedItems = new List<VwItem>();
        }
        public VwItem Item { get; set; }
        public List<TbItemImage> LstItemImages { get; set; }
        public List<VwItem> LstRecommendedItems { get; set; }

    }
}
