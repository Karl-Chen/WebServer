using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyModel_CodeFirst.Models
{
    public class Book
    {
        [Key]
        [Display(Name = "編號")]
        [StringLength(36, MinimumLength =36)]       //固定長度為是32
        //[RegularExpression("1[1-9][0-9]{4}", ErrorMessage = "格式錯誤(1XXXXX)")]
        public string BookID { get; set; } = null!;      // 採GUID

        public long SN { set; get; }

        [Display(Name = "主題")]
        [StringLength(30,MinimumLength = 1, ErrorMessage ="主題1~30個字")]
        [Required(ErrorMessage = "必填")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "必填")]
        [Display(Name = "內容")]
        [DataType(DataType.MultilineText)]                  //多行
        public string Description { get; set; } = null!;     //沒寫StringLength就是最大長度

        [Display(Name = "作者")]
        [StringLength(20, ErrorMessage = "作者最多20字")]
        [Required(ErrorMessage = "必填")]
        public string Author { get; set; } = null!;
        [Display(Name = "發佈時間")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd hh:mm:ss}")]
        [HiddenInput]
        public DateTime TimeStmp { get; set; }
        [HiddenInput]
        public string? PhotoType { get; set; }
        [Display(Name ="照片")]
        [StringLength(44)]
        public string? Photo { get; set; }      //照片上傳檔名為GUID+原本的副檔名 XXXXXXXXXXXXXXXXXXX.jpg

        //1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯
        public virtual List<Rebook>? ReBooks { get; set; }
    }
}
