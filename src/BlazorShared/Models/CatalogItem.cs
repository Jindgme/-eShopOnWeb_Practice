using Microsoft.AspNetCore.Components.Forms;

namespace BlazorShared.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public int CatalogTypeId { get; set; }
        public string CatalogType { get; set; } = "NotSet";

        public int CatalogBrandId { get; set; }
        public string CatalogBrand { get; set; } = "NotSet";

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
        public string PictureBase64 { get; set; }
        public string PictureName { get; set; }
        private const int ImageMaximumBytes = 512000;
        public static IEnumerable<string> IsValidImage(IBrowserFile file)
        {
            var errors= new List<string>();
            if (file==null)
            {
                errors.Add("文件未找到");
                return errors;
            }
            if (file.Size <= 0)
            {
                errors.Add("文件的大小为0");
            }
            if (file.Size > ImageMaximumBytes)
            {
                errors.Add("文件大小不能超过512KB");
            }
            if (!IsExtensionValid(file.Name))
            {
                errors.Add("文件上传不是图片格式");
            }
            return errors;
        }
        public static async Task<string> DataToBase64(IBrowserFile file)
        {
            using var stream=file.OpenReadStream(ImageMaximumBytes);
            using var ms=new MemoryStream();
            await stream.CopyToAsync(ms);
            var fileData=ms.ToArray();
            return Convert.ToBase64String(fileData);
        }
        private static bool IsExtensionValid(string fileName)
        {
            var extension=Path.GetExtension(fileName);
            return string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase) ||
                   string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
        }
    }
}
