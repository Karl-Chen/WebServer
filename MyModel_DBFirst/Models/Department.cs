using System.ComponentModel.DataAnnotations;

namespace MyModel_DBFirst.Models
{
    public class Department
    {
        [Key]//主索引鍵
        [Display(Name = "科系代碼")]
        public string DeptID { get; set; } = null!;

        [Display(Name = "科系名稱")]
        public string DeptName { get; set;} = null!;
        public virtual List<tStudent>? students { get; set; }       //這個物件是在描述它與tStudent是一對多的關聯(不完全參與，因為科系可能會沒學生)，而且這是虛擬屬性(讓程式碼自己去做join)
    }
}
