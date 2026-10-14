using BusinessLayer.Bl.Interfaces;
using BusinessLayer.Data;
using Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Bl.Classes
{
        public class ItemImage : IitemImage
    {
            private readonly LapShopContext _context;
            public ItemImage(LapShopContext context)
            {
                _context = context;
            }

        public List<TbItemImage> GetImageByItemId(int itemId)
        {
            try
            {
                var image = _context.TbItemImages.Where(i => i.ItemId == itemId).ToList();
                if (image == null)
                {
                    throw new Exception($"No image found for item with ID {itemId}");
                }
                return image;
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                throw new Exception($"An error occurred while retrieving the image for item with ID {itemId}: {ex.Message}", ex);
            }
        }
    }

}
