using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseSystem.Infrastructure.Dtos
{
    public class ProductJson
    {
        [JsonProperty("products")]
        public List<ProductDto> productDto { get; set; }
    }
    public class ProductDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double? Price { get; set; }
        [JsonProperty("contain_articles")]
        public HashSet<ContainArticles> ContainArticles { get; set; }

    }

    public class ContainArticles
    {
        [JsonProperty("art_id")]
        public string ArticleId { get; set; }
        [JsonProperty("amount_of")]
        public string ArticleCount { get; set; }
    }
    public class AvailableProductDto
    {
        [JsonProperty("product_name")]
        public string Name { get; set; }
        [JsonProperty("quantity")]
        public int ProductCount { get; set; }

    }

    public class ProductCartDto
    {
        [JsonProperty("product_name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double? Price { get; set; }
        [JsonProperty("quantity")]
        public int ProductCount { get; set; }

    }

    public class SoldProduct
    {
        [JsonProperty("product")]
        public string Name { get; set; }
        [JsonProperty("narration")]
        public string Description { get; set; }
    }

}
