using Domains.Models;

namespace LapShop.ViewModels
{
    public class VmAdminDashboard
    {
        public int ItemsCount { get; set; }
        public int CategoriesCount { get; set; }
        public int SlidersCount { get; set; }
        public List<VwItem> LatestItems { get; set; } = new List<VwItem>();
    }
}
