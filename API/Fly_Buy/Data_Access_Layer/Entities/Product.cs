﻿namespace Data_Access_Layer.Repository.Entities
{
    public class Product
  {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Ratings { get; set; }

    }
}
