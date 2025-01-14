using System.ComponentModel.DataAnnotations;

namespace MyModel_CodeFirst.Models
{
    public class Login
    {
        //5.1.4 設計Login類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(DisplayName)
        [Key]
        [Display(Name = "帳號")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "帳號為5-10碼")]
        [RegularExpression("[A-Za-z][A-Za-z0-9]{4,9}", ErrorMessage = "帳號格式有誤")]
        [Required(ErrorMessage ="必填")]
        public string Account { get; set; } = null!;
        [Display(Name = "密碼")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "密碼為8-16碼")]
        [MinLength(8)]
        [MaxLength(16)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必填")]
        public string Password { get; set; } = null!;
    }
}
