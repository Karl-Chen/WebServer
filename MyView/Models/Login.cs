using System.ComponentModel.DataAnnotations;

namespace MyView.Models
{
    public class Login
    {
        [Display(Name = "帳號")]
        [Required(ErrorMessage ="必填")]
        [StringLength(20)]
        public string Account { get; set; }=string.Empty;
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="最少8碼")]
        [MaxLength(20, ErrorMessage = "最多20碼")]
        public string Password { get; set; } = string.Empty;
    }
}
