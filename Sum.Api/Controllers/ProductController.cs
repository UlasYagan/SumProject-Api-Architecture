using log4net;
using Microsoft.AspNetCore.Mvc;
using Sum.Model;
using Sum.Model.Dtos;
using Sum.Model.Options;
using Sum.Service.Interface;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Sum.Api.ServiceExtension;

namespace Sum.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ProductController));
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(nameof(GetProductList))]
        public ActionResult<SumResultModel<ProductListDto>> GetProductList([FromQuery(Name = "ps")] int? pageSize, [FromQuery(Name = "pn")] int? pageNumber)
        {
            try
            {
                return _productService.GetProductList(null, new DataPagingOptions(pageSize, pageNumber));
            }   
            catch (Exception ex)
            {
                _log.Error($"ProductController GetProductList method - {ex.Message}",ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<SumResultModel<ProductDto>> GetProductById(int id)
        {
            try
            {
                return _productService.GetProductById(id);
            }
            catch (Exception ex)
            {
                _log.Error($"ProductController GetProductById method - {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(ProductCreate))]
        public ActionResult<SumResultModel<ProductDto>> ProductCreate([FromBody] ProductDto entity)
        {
            try
            {
                return _productService.CreateProduct(entity);
            }
            catch (Exception ex)
            {
                _log.Error($"ProductController ProductCreate method - {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(nameof(ProductUpdate))]
        public ActionResult<SumResultModel<ProductDto>> ProductUpdate([FromBody] ProductDto entity)
        {
            try
            {
                return _productService.UpdateProduct(entity);
            }
            catch (Exception ex)
            {
                _log.Error($"ProductController ProductUpdate method - {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ProductDelete/{id}")]
        public ActionResult<SumResultModel<bool>> ProductDelete(int id)
        {
            try
            {
                return _productService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                _log.Error($"ProductController ProductDelete method - {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

    }
}