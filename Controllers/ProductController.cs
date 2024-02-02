using Assignment_API.Data;
using Assignment_API.Models;
using Assignment_API.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            IEnumerable<Product> productList = await _db.Products.ToListAsync();
            return Ok(_mapper.Map<List<ProductDto>>(productList));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductCreateDto product)
        {
            if (await _db.Products.FirstOrDefaultAsync(u => u.ProductName.ToLower() == product.ProductName.ToLower()) != null)
            {
                ModelState.AddModelError("", "Product already Exist");
                return BadRequest(ModelState);
            }
            if (product == null)
            {
                return BadRequest(product);
            }

            Product model = _mapper.Map<Product>(product);

            await _db.Products.AddAsync(model);
            await _db.SaveChangesAsync();

            return Ok(model);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProuct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto product)
        {
            if (product == null || id != product.ProductId)
            {
                return BadRequest();
            }

            Product model = _mapper.Map<Product>(product);

            await _db.Products.AddAsync(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
