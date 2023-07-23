using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PressCore.DBContext;
using PressCore.Entities;
using PressCore.Interfaces;
using PressCore.Specification;
using PressInfrastructure.Data;
using PressInfrastructure.DTO.ResponseDto;
using PressInfrastructure.Errors;
using PressInfrastructure.Helper;

namespace DigitalPress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPressRepository<Product> _repo;
        private readonly IMapper _mapper;

        public ProductController(IPressRepository<Product> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        //[ProducesResponseType]
        public async Task<ActionResult<Pagination<ProductReturnDto>>> GetProducts([FromQuery] ProductSpecParam productParams)
        {
            var detailProduct = new ProductWithTypesAndBrandSpecification(productParams);
            var countSpec = new ProductWithFilterForCountSpecification(productParams);
            var totalItems = await _repo.CountAsync(detailProduct);
            var products = await _repo.ListAsync(detailProduct);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReturnDto>>(products);
            return Ok(new Pagination<ProductReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));

            /*return products.Select(product =>  new ProductReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            }).ToList();*/
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductReturnDto>> GetProduct(string id)
        {
            var detailProduct = new ProductWithTypesAndBrandSpecification();
             var product = await _repo.GetEntityWithSpec(detailProduct);
            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductReturnDto>(product);
/*            return new ProductReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            };*/
        }


      /*  [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }*/
    }
}
