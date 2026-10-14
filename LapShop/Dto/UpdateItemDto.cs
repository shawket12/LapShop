using System.ComponentModel.DataAnnotations;

namespace LapShop.Dto
{
    public class UpdateItemDto
    {
        [Required]
        public string ItemName { get; set; } = null!;

        
        [Range(50, 100000)]
        public decimal PurchasePrice { get; set; }

      
        [Range(50, 100000)]
        public decimal SalesPrice { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        public int? ItemTypeId { get; set; }

        [Required]
        public int? OsId { get; set; }

        
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
