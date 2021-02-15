using BigShop.Models.Product;
using BigShop.Models.Warehouse_Product;
using BigShop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigShop.Web.Controllers
{
    [Route("api/[controller]")] //interferes with individual routes set for actions
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IWarehouse_ProductRepository _warehouse_ProductRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository,
                                  IOrderLineRepository orderLineRepository,
                                  IWarehouse_ProductRepository warehouse_ProductRepository,
                                  IWarehouseRepository warehouseRepository,
                                  IManufacturerRepository manufacturerRepository,
                                  ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _orderLineRepository = orderLineRepository;
            _warehouse_ProductRepository = warehouse_ProductRepository;
            _warehouseRepository = warehouseRepository;
            _manufacturerRepository = manufacturerRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateProduct([FromBody] ProductClientCreate productClientCreate)
        {
            if (ModelState.IsValid == false)
            {
                return StatusCode(400, ModelState);
            }

            var manufacturer = await _manufacturerRepository.GetByIdAsync(productClientCreate.Product.Manufacturer_Id);
            if (manufacturer == null)
            {
                return BadRequest("No manufacturer with that Id");
            }

            var category = await _categoryRepository.GetByIdAsync(productClientCreate.Product.Category_Id);
            if (category == null)
            {
                return BadRequest("No category with that Id");
            }

            var warehouse = await _warehouseRepository.GetByIdAsync(productClientCreate.QuantityAndWarehouse.Warehouse_Id);
            if (warehouse == null)
            {
                return BadRequest("No warehouse with that Id");
            }
                         
            int newProductId = await _productRepository.CreateAsync(productClientCreate.Product);
            if (newProductId != -1)
            {
                var newProduct = await _productRepository.GetByIdAsync(newProductId);
                // Establish relation between warehouse and product and product quantity in that warehouse
                Warehouse_ProductCreate warehouseProduct = new Warehouse_ProductCreate
                {
                    Quantity = productClientCreate.QuantityAndWarehouse.Quantity,
                    Warehouse_Id = productClientCreate.QuantityAndWarehouse.Warehouse_Id,
                    Product_Id = newProduct.Id
                };
                  
                warehouseProduct.Product_Id = newProduct.Id;

                await _warehouse_ProductRepository.CreateAsync(warehouseProduct);

                return CreatedAtRoute("GetByProductId", new { productId = newProductId }, newProduct);
            }

            return StatusCode(500);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound($"Product with Id {productId} does not exist");
            }

            var orderlines = await _orderLineRepository.GetByProductIdAsync(productId);
            if (orderlines.Count != 0)
            {
                return BadRequest("Cannot remove products that have been ordered");
            }

            await _warehouse_ProductRepository.DeleteByProductIdAsync(productId);
            int affectedRows = await _productRepository.DeleteAsync(productId);
            if (affectedRows == 1)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            if (products == null)
            {
                return StatusCode(500);
            }

            if (products.Count == 0)
            {
                return Ok("No products in database");
            }

            return Ok(products);
        }

        [HttpGet("{productId}", Name = "GetByProductId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetByProductId(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound($"Product with Id {productId} does not exist");
            }
            return Ok(product);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProduct(int productId, [FromBody] Product updatedProduct)
        {
            if (productId != updatedProduct.Id)
            {
                return BadRequest("Product Id does not match");
            }

            var manufacturer = await _manufacturerRepository.GetByIdAsync(updatedProduct.Manufacturer_Id);
            if (manufacturer == null)
            {
                return BadRequest("No manufacturer with that Id");
            }

            var category = await _categoryRepository.GetByIdAsync(updatedProduct.Category_Id);
            if (category == null)
            {
                return BadRequest("No category with that Id");
            }

            var oldProduct = await _productRepository.GetByIdAsync(productId);
            if (oldProduct == null)
            {
                return NotFound($"product with Id {productId} not found");
            }

            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            int affectedRows = await _productRepository.UpdateAsync(updatedProduct);

            if (affectedRows == 1)
            {
                return Ok();
            }

            return StatusCode(500);
        }
         
        [HttpGet("{productId}/warehouses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Warehouse_Product>>> GetWarehousesWithProduct(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound($"Product with Id {productId} does not exist");
            }

            var warehouseProducts = await _warehouse_ProductRepository.GetByProductIdAsync(productId);
            if (warehouseProducts.Count == 0)
            {
                return BadRequest("Warehouse has no products");
            }
            return Ok(warehouseProducts);
        }
    }
}
