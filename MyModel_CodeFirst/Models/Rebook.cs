using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyModel_CodeFirst.Models
{
    public class Rebook
    {
        [Key]
        [Display(Name = "編號")]
        [StringLength(36, MinimumLength = 36)]       //固定長度為是32
        //[RegularExpression("1[1-9][0-9]{4}", ErrorMessage = "格式錯誤(1XXXXX)")]
        public string ReBookID { get; set; } = null!;      // 採GUID

        public long SN { set; get; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "回覆內容")]
        [DataType(DataType.MultilineText)]                  //多行
        public string Description { get; set; } = null!;     //沒寫StringLength就是最大長度

        [Display(Name = "回覆者")]
        [StringLength(20, ErrorMessage = "回覆者最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Author { get; set; } = null!;
        [Display(Name = "回覆時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}")]
        [HiddenInput]
        public DateTime TimeStmp { get; set; }
        [ForeignKey("Book")]
        public string BookID { get; set; } = null!;

        public virtual Book? Book { get; set; } = null!;
    }
}
