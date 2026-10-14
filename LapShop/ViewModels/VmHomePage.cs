using Domains.Models;

namespace LapShop.ViewModels
{
    
    public class VmHomePage
    {
        public VmHomePage()
        {
            LstAllItems= new List<VwItem>();
            LstNewItems = new List<VwItem>();
            LstSliders = new List<TbSlider>();
        }
        public List<VwItem> LstAllItems { get; set; }
        public List<VwItem> LstNewItems { get; set; }
        public List<TbSlider>? LstSliders { get; set; }


    }
}
