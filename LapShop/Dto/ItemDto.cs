namespace LapShop.Dto
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public decimal SalesPrice { get; set; }
        public string? ImageName { get; set; }

        public int CategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public int? OsId { get; set; }

        public int? RamSize { get; set; }
        public string? Processor { get; set; }
        public string? Gpu { get; set; }
        public string? HardDisk { get; set; }
        public string? ScreenReslution { get; set; }
        public string? ScreenSize { get; set; }
        public string? Weight { get; set; }
        public string? Description { get; set; }
    }
}
