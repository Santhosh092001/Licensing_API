using Licensing.IRepositories;
using Licensing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Licensing.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var sendProduct = _productRepo.addProduct(product);
            if(sendProduct != null)
                return Ok(new { message = "Product Created Successfully" });
            else
                return BadRequest("Product not Created");
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            var products = _productRepo.getProduct();
            return Ok(products);
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var updateproduct = _productRepo.updateProduct(product);
            if (updateproduct == true)
                return Ok(new { message = "Product Updated Successfully"});
            else
                return BadRequest("Product not Updated");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var deleteId = _productRepo.deleteProduct(id);
            if(deleteId == true)
                return Ok(new { message = "Product Deleted Successfully" });
            return BadRequest("Product not Deleted");
        }


        [HttpGet]
        public IActionResult getProductsName()
        {
            var productId = _productRepo.getProductsName();
            return Ok(productId);
        }

    }
}
