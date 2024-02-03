﻿using Assignment_API.Models;
using Assignment_API.Models.Dto;
using Assignment_API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Assignment_API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected APIResponseDto _response;
        private readonly IProductRepository _dbProduct;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository dbProduct, IMapper mapper)
        {
            _dbProduct = dbProduct;
            _mapper = mapper;
            _response = new APIResponseDto();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponseDto>> GetProducts()
        {
            try
            {
                IEnumerable<Product> productList = await _dbProduct.GetAllAsync();
                _response.Result = _mapper.Map<List<ProductDto>>(productList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponseDto>> GetProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var product = await _dbProduct.GetAsync(u => u.ProductId == id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ProductDto>(product);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponseDto>> CreateProduct([FromBody] ProductCreateDto product)
        {

            try
            {
                if (await _dbProduct.GetAsync(u => u.ProductName.ToLower() == product.ProductName.ToLower()) != null)
                {
                    ModelState.AddModelError("", "Product already Exist");
                    return BadRequest(ModelState);
                }
                if (product == null)
                {
                    return BadRequest(product);
                }

                Product model = _mapper.Map<Product>(product);

                await _dbProduct.CreateAsync(model);

                _response.Result = _mapper.Map<ProductDto>(model);
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        public async Task<ActionResult<APIResponseDto>> DeleteProuct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var product = await _dbProduct.GetAsync(u => u.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
                await _dbProduct.RemoveAsync(product);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponseDto>> UpdateProduct(int id, [FromBody] ProductUpdateDto product)
        {
            try
            {
                if (product == null || id != product.ProductId)
                {
                    return BadRequest();
                }

                Product model = _mapper.Map<Product>(product);

                await _dbProduct.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
