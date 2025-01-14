using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyWebAPI.Models;

public partial class Category
{
    public string CateID { get; set; } = null!;

    public string CateName { get; set; } = null!;

    //JsonIgnore 是忽略，不要輸出到回傳的Josn裡面
    [JsonIgnore]
    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
