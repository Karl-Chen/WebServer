using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyWebAPI.Models;

public partial class Product
{
    public string ProductID { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string Picture { get; set; } = null!;

    public string CateID { get; set; } = null!;

    //[JsonIgnore]
    //5.1.4 修改Product.cs的Category屬性為非必填
    public virtual Category? Cate { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

}
