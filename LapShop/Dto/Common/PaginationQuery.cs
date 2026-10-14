using LapShop.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Dto.Common
{
    public class PaginationQuery
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
        [AllowedSortBy("ItemId", "ItemName", "SalesPrice")]
        public string? SortBy { get; set; } = "ItemId";

    }

}
