namespace LapShop.ViewModels
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            lstItems = new List<VmShoppingCartItem>();

        }
        public List<VmShoppingCartItem> lstItems { get; set; }
        public decimal TotalPrice { get; set; }
        public string PromoCode { get; set; }

    }
}
