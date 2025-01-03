using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyModel_DBFirst.Models;

public partial class tStudent
{
    //3.2 撰寫在View上顯示的欄位內容(Display)
    //3.3 撰寫在表單上的欄位驗證規則(需using System.ComponentModel.DataAnnotations)
    //    常用的驗證器 Required、StringLength、RegularExpression、Compare、EmailAddress、Range、DataType
    //    Required:必填欄位
    //    StringLength:資料字數
    //    RegularExpression:資料格式
    //    Compare:與其它欄位比較是否相等
    //    EmailAddress:是否是E - mail格式
    //    Range: 限制所填的範圍
    [Display(Name = "學號")]
    [Required(ErrorMessage ="必填")]
    [RegularExpression("1[1-9][0-9]{4}", ErrorMessage = "格式錯誤(1XXXXX)")]
    public string fStuId { get; set; } = null!;

    [Display(Name = "姓名")]
    [Required(ErrorMessage = "必填")]
    [StringLength(30, ErrorMessage = "姓名最多30個字")]
    public string fName { get; set; } = null!;

    [Display(Name = "E-mail")]
    [StringLength(40, ErrorMessage = "信箱最多40個字")]
    [EmailAddress(ErrorMessage = "E-mail格式錯誤")]
    public string? fEmail { get; set; }

    [Display(Name = "成績")]
    [Range(0, 100, ErrorMessage = "請填0~100的整數")]
    public int? fScore { get; set; }


    [Display(Name = "科系")]
    [ForeignKey("Department")]
    public string DeptID { get; set; } = null!;     //這是foreign key

    public virtual Department? Department { get; set; }     //一位學生只有一個科系，這是個虛擬屬性，這個變數只是用來描述tStudent跟Department的關係(讓程式碼自己去做join)
}
