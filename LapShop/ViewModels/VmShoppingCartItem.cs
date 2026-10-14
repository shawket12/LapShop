namespace LapShop.ViewModels
{
    public class VmShoppingCartItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ImageName { get; set; }
        public int qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

    }
}
