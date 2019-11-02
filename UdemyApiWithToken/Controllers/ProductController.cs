using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyApiWithToken.Domain;
using UdemyApiWithToken.Domain.Responses;
using UdemyApiWithToken.Domain.Services;
using UdemyApiWithToken.Extensions;
using UdemyApiWithToken.Resources;

namespace UdemyApiWithToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //  Get  /localhost:3333/api/product/2

        // Post   /localhost:3333/api/product

        // put   /localhost:3333/api/product /request body
        //delete   /api/product/removeProduct/2
        // private readonly IProductService productService;
        private readonly IGenericService<Product> productService;

        private readonly IMapper mapper;

        public ProductController(IGenericService<Product> productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            BaseResponse<IEnumerable<Product>> productListResponse = await productService.GetWhere(x => x.Id > 0);

            if (productListResponse.Success)
            {
                return Ok(productListResponse.Extra);
            }
            else
            {
                return BadRequest(productListResponse.ErrorMessage);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            BaseResponse<Product> productResponse = await productService.GetById(id);

            if (productResponse.Success)
            {
                return Ok(productResponse.Extra);
            }
            else
            {
                return BadRequest(productResponse.ErrorMessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = mapper.Map<ProductResource, Product>(productResource);

                var Response = await productService.Add(product);

                if (Response.Success)
                {
                    return Ok(Response.Extra);
                }
                else
                {
                    return BadRequest(Response.ErrorMessage);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = mapper.Map<ProductResource, Product>(productResource);

                product.Id = id;

                var response = await productService.Update(product);

                if (response.Success)
                {
                    return Ok(response.Extra);
                }
                else
                {
                    return BadRequest(response.ErrorMessage);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            BaseResponse<Product> response = await productService.Delete(id);

            if (response.Success)
            {
                return Ok(response.Extra);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }
    }
}