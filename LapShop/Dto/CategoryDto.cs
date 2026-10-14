namespace LapShop.Dto
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool ShowInHomePage { get; set; }
        public string? ImageName { get; set; }
    }
}
