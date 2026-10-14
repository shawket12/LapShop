using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.ViewModels
{
    public class CategoryViewModel
    {
        [ValidateNever]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter category name")]

        public string CategoryName { get; set; } = null!;
        public bool ShowInHomePage { get; set; }
        [ValidateNever]
        public string ImageName { get; set; }
        [ValidateNever]

        public string CreatedBy { get; set; } = null!;
        [ValidateNever]

        public DateTime CreatedDate { get; set; }

    }
}
