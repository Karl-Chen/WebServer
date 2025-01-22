﻿namespace MyWebAPI.DTOs
{
    public class ProductDTO
    {
        public string ProductID { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string CateID { get; set; } = null!;

        public string CateName { get; set; } = null!;

        public string Picture { get; set;} = null!;
    }
}
