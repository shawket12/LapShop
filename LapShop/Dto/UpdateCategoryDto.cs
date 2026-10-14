using System.ComponentModel.DataAnnotations;

namespace LapShop.Dto
{
    public class UpdateCategoryDto
    {
        [Required]
        public string CategoryName { get; set; }
        public bool ShowInHomePage { get; set; }
    }
    
}
