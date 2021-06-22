using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseSystem.Infrastructure.Dtos
{
    public class InventoryJson
    {
        [JsonProperty("inventory")]
        public List<InventoryDto> inventoryDto { get; set; }
    }
    public class InventoryDto
    {
        [JsonProperty("art_id")]
        public int ArtId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("stock")]
        public int Stock { get; set; }
    }
}
