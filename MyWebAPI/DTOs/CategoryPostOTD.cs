using System.ComponentModel.DataAnnotations;
using MyWebAPI.Models;
using static MyWebAPI.ValidationAttributes.MyValidator;

namespace MyWebAPI.DTOs
{
    public class CategoryPostOTD
    {
        [Required]
        public string CateID { get; set; } = null!;
        [Required]
        [CateNameCheck]
        [StringLength(20)]
        public string CateName { get; set; } = null!;
    }

    
}
