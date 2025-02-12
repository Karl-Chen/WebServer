using System.ComponentModel.DataAnnotations;
using static MyWebAPI.ValidationAttributes.MyValidator;

namespace MyWebAPI.DTOs
{
    public class CategoryPutOTD
    {
        [Required]
        [CateNameCheck]
        [StringLength(20)]
        public string CateName { get; set; } = null!;
    }
}
