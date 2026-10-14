namespace LapShop.Dto
{
    public class ItemListDto
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public decimal SalesPrice { get; set; }

        public string? ImageName { get; set; }
    }
}