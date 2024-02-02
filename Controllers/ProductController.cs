using Assignment_API.Data;
using Assignment_API.Models;
using Assignment_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(u => u.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDto> CreateProduct([FromBody] ProductCreateDto product)
        {
            if (_db.Products.FirstOrDefault(u => u.ProductName.ToLower() == product.ProductName.ToLower()) != null)
            {
                ModelState.AddModelError("", "Product already Exist");
                return BadRequest(ModelState);
            }
            if (product == null)
            {
                return BadRequest(product);
            }

            Product model = new()
            {
                Active = product.Active,
                ProductName = product.ProductName,
                SKU = product.SKU,
                Created = product.Created,
                RetailPrice = product.RetailPrice,
                SalePrice = product.SalePrice,
                LowestPrice = product.LowestPrice,
            };


            _db.Products.Add(model);
            _db.SaveChanges();

            return Ok(model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public IActionResult DeleteProuct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(u => u.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateDto product)
        {
            if (product == null || id != product.ProductId)
            {
                return BadRequest();
            }
            Product model = new()
            {
                ProductId = product.ProductId,
                Active = product.Active,
                ProductName = product.ProductName,
                SKU = product.SKU,
                RetailPrice = product.RetailPrice,
                SalePrice = product.SalePrice,
                LowestPrice = product.LowestPrice,
            };
            _db.Products.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
