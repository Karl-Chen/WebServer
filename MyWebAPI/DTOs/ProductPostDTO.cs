using System.ComponentModel.DataAnnotations;
using static MyWebAPI.ValidationAttributes.MyValidator;

namespace MyWebAPI.DTOs
{
    //5.2.3 建立一個ProductPostDTO給Post利用DTO傳遞資料
    public class ProductPostDTO
    {
        [Required]
        [RegularExpression("[A-Z][1-9][0-2]{3}")]
        public string ProductID { get; set; } = null!;
        [Required]
        [StringLength(40)]
        //5.3.4 在需要使用此驗證器的屬性上加入標籤(這裡範例為ProductName屬性)
        [ProductNameCheck]
        public string ProductName { get; set; } = null!;
        [Required]
        [Range(0, 999999)]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
        [Required]
        //因為要上傳檔案，所以變數類別要改成IFormFile
        public IFormFile Picture { get; set; } = null!;
        [Required]
        [RegularExpression("[A-Z][1-9]")]
        public string CateID { get; set; } = null!;
    }

    
}
