using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstModelExam.Models
{
    public class Rebook
    {
        [Key]
        [StringLength(36, MinimumLength = 36)]
        public string RebookID { get; set; } = null!;

        [Required(ErrorMessage = "回覆內容必填")]
        [Display(Name = "回覆內容")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null!;

        [Display(Name = "作者")]
        [StringLength(20, ErrorMessage = "作者最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Author { get; set; } = null!;
        [Display(Name = "回文時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd hh:mm:ss.fff}")]
        [HiddenInput]
        public DateTime TimeStmp { get; set; }

        public string BookID { get; set; } = null!;
        public virtual Book? Books { get; set; }
    }
}
