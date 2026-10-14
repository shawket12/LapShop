namespace LapShop.Utlities
{
    public static class Helper
    {
        public static async Task<string> UploadImage(
            IFormFile file,
            string folderName)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string extension = Path.GetExtension(file.FileName);

            string imageName = Guid.NewGuid().ToString() + extension;

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Uploads",
                folderName,
                imageName
            );

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return imageName;
        }

        public static void DeleteImage(string imageName, string folderName)
        {
            if (string.IsNullOrEmpty(imageName))
                return;

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Uploads",
                folderName,
                imageName
            );

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
       
    }
}