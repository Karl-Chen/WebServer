using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstModelExam.Models
{
    public class Book
    {
        [Key]
        [StringLength(36, MinimumLength = 36)]
        public string BookID { get; set; } = null!;

        [Display(Name = "主題")]
        [StringLength(50, MinimumLength = 1, ErrorMessage ="主題限制字數為1~50個字")]
        [Required(ErrorMessage = "必填")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "必填")]
        [Display(Name = "內容")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;

        [Display(Name = "作者")]
        [StringLength(20, ErrorMessage = "作者最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Author { get; set; } = null!;
        [Display(Name = "發佈時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        [HiddenInput]
        public DateTime TimeStmp { get; set; }
        [HiddenInput]
        public string? PhotoType { get; set; }
        [Display(Name = "照片")]
        [StringLength(44)]
        public string? Photo { get; set; }
    }
}
