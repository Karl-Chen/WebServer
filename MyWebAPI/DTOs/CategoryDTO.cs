using MyWebAPI.Models;

namespace MyWebAPI.DTOs
{
    public class CategoryDTO
    {
        public string CateID { get; set; } = null!;

        public string CateName { get; set; } = null!;


        public  ICollection<Product> Product { get; set; } = new List<Product>();

    }
}
