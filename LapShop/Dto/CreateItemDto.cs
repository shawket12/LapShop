using System.ComponentModel.DataAnnotations;

namespace LapShop.Dto
{
    public class CreateItemDto
    {
        [Required]
        public string ItemName { get; set; } = null!;

        [Required]
        [Range(50, 100000)]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(50, 100000)]
        public decimal SalesPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int? ItemTypeId { get; set; }

        [Required]
        public int? OsId { get; set; }

        [Required]
        [Range(1, 500)]
        public int? RamSize { get; set; }

        public string? Processor { get; set; }

        public string? Gpu { get; set; }

        public string? HardDisk { get; set; }

        public string? ScreenReslution { get; set; }

        public string? ScreenSize { get; set; }

        public string? Weight { get; set; }

        public string? Description { get; set; }

        public string? ImageName { get; set; }
    }
}
