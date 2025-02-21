using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodStore.Models;

public partial class Product
{
    public string ProductID { get; set; } = null!;
    [Display(Name = "產品名稱")]
    public string ProductName { get; set; } = null!;
    [Display(Name = "價格")]
    //C是貨幣格式，C0是貨幣格式呈現到小數第0位
    [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
    public decimal Price { get; set; }
    [Display(Name ="商品描述")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    public string Picture { get; set; } = null!;

    public string CateID { get; set; } = null!;

    public virtual Category Cate { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
}
