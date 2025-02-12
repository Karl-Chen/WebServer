using System.ComponentModel.DataAnnotations;
using static MyWebAPI.ValidationAttributes.MyValidator;

namespace MyWebAPI.DTOs
{
    public class ProductPutDTO
    {
        [StringLength(40)]
        [ProductNameCheck]
        public string? ProductName { get; set; } = null!;

        [Range(0, 999999)]
        public decimal? Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        //因為要上傳檔案，所以變數類別要改成IFormFile
        public IFormFile? Picture { get; set; } = null!;
        
    }
}
