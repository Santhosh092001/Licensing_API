using Licensing.Dto;
using Licensing.IRepositories;
using Licensing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Licensing.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductCustomerMapController : ControllerBase
    {
        private readonly IProductCustomerMapRepository _productCustomerMapRepository;
        public ProductCustomerMapController(IProductCustomerMapRepository productCustomerMapRepository)
        {
            _productCustomerMapRepository = productCustomerMapRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var productCustomer = _productCustomerMapRepository.get();
            return Ok(productCustomer);
        }

        [HttpPost]
        public IActionResult CreateProductCustomerMap(ProductCustomerDto newproductCustomerMap)
        {
            var productCustomerMap = new ProductCustomerMap();
            productCustomerMap.ProductId = newproductCustomerMap.ProductId;
            productCustomerMap.CustomerId = newproductCustomerMap.CustomerId;
            var productCustomer = _productCustomerMapRepository.createProductCustomerMap(productCustomerMap);
            if(productCustomer != null)
            {
                return Ok(new { message = "ProductCustomerMap Created Successfully"});
            }
            else
            { 
                return BadRequest("ProductCustomerMap not Created");
            }
        }


        [HttpPut]
        public IActionResult UpdateProductCustomerMap(ProductCustomerMap updateMap)
        {
            var updationMap = _productCustomerMapRepository.updateProductCustomerMap(updateMap);
            if(updationMap != true)
            {
                return Ok(new { message = "ProductCustomerMap Updated Successfully" });
            }
            else
            {
                return BadRequest("ProductCustomerMap not Updated");
            }
        }

        [HttpGet]
        public IActionResult GetProductCustomersId()
        {
            var productCustomersId = _productCustomerMapRepository.GetProductCustomersId();
            if(productCustomersId != null)
            {
                return Ok(productCustomersId);
            }
            return BadRequest();
        }




        /*---------------Join Linq Query----------------*/

        [HttpGet]
        public IActionResult Gets()
        {
            var productCustomer = _productCustomerMapRepository.gets();
            return Ok(productCustomer);
        }


    }
}
