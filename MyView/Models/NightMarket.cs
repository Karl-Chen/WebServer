using System.ComponentModel.DataAnnotations;

namespace MyView.Models
{
    public class NightMarket
    {
        //標籤表達法
        [Display(Name="夜市代碼")]
        public string ID { get; set; } = string.Empty;
        [Display(Name = "夜市名稱")]
        public string Name { get; set; } = string.Empty ;
        [Display(Name = "夜市地址")]
        public string Address { get; set; } = string.Empty; 
    }
}
