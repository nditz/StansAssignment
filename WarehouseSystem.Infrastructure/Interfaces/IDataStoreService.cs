using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WarehouseSystem.Infrastructure.Dtos;

namespace WarehouseSystem.Infrastructure.Interfaces
{
    public interface IDataStoreService
    {
        InventoryDto AddInventory(InventoryDto inventoryDto);
        IEnumerable<InventoryDto> GetAllInventoryData();
        ProductDto AddProduct(ProductDto productDto);
        IEnumerable<ProductDto> GetAllProductData();
        IEnumerable<AvailableProductDto> GetAllAvailableProductData();
        SoldProduct SellProduct(ProductCartDto productCartDto);
    }
}
