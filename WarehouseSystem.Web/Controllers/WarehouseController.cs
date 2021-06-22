using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WarehouseSystem.Infrastructure.Dtos;
using WarehouseSystem.Infrastructure.Interfaces;

namespace WarehouseSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IDataStoreService dataStoreService;
        public WarehouseController(IDataStoreService dataStoreService)
        {
            this.dataStoreService = dataStoreService;
        }

        //GET api/warehouse/articles
        [HttpGet("articles", Name = "articles")]
        public IActionResult GetInventory()
        {
            var result = this.dataStoreService.GetAllInventoryData();
            if (result != null )
            {
                return Ok(result);
            }
            return NotFound(new { message = "Upload inventory!" });
        }

        //GET api/warehouse/products
        [HttpGet("products", Name = "products")]
        public IActionResult GetProducts()
        { 
            var result = this.dataStoreService.GetAllProductData();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { message = "Upload products!" });
        }

        //GET api/warehouse/available/products
        [HttpGet("available/products", Name = "available/products")]
        public IActionResult GetAvailableProducts()
        { 
            var result = this.dataStoreService.GetAllAvailableProductData();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { message = "Upload products!" });
        }

        //GET api/warehouse/sell/products
        [HttpPut("sell/products", Name = "sell/products")]
        public IActionResult SellProducts([FromBody] ProductCartDto productCartDto)
        {
            var result = this.dataStoreService.SellProduct(productCartDto);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { message = "Upload products!" });
        }



        //POST api/warehouse/upload/articles
        [HttpPost("upload/articles", Name = "upload/articles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadInventory(IFormFile file, CancellationToken cancellationToken)
        {
            bool isSuccess;
            string filePath;
            if (CheckIfJsonFile(file))
            {
                (isSuccess, filePath) = await WriteFile(file);
                if (isSuccess)
                {
                    var jsonString = System.IO.File.ReadAllText(filePath);
                    var model = JsonConvert.DeserializeObject<InventoryJson>(jsonString);
                    foreach(var item in model.inventoryDto)
                        this.dataStoreService.AddInventory(item);
                    return Ok(new { message = "Inventory file uploaded successfully!" });
                }
                return BadRequest(new { message = "Inventory file upload failed!" });
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }
        }

        //POST api/warehouse/upload/products
        [HttpPost("upload/products", Name = "upload/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadProducts(IFormFile file, CancellationToken cancellationToken)
        {
            bool isSuccess;
            string filePath;
            if (CheckIfJsonFile(file))
            {
                (isSuccess, filePath) = await WriteFile(file);
                if (isSuccess)
                {
                    var jsonString = System.IO.File.ReadAllText(filePath);
                    var model = JsonConvert.DeserializeObject<ProductJson>(jsonString);
                    foreach (var item in model.productDto)
                        this.dataStoreService.AddProduct(item);
                    return Ok(new { message = "Product file uploaded successfully!" });
                }
                return BadRequest(new { message = "Product file upload failed!" });
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension" });
            }
        }

        private async Task<(bool Worked, string Result)> WriteFile(IFormFile file)
        {
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return (true, path);
            }
            catch (Exception e)
            {
                //log error
            }

            return (false, "");
        }

        private bool CheckIfJsonFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".json"); 
        }

    }
}
