﻿namespace KC.WebApi.Models.Product
{
    public class GetProductResponse
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
