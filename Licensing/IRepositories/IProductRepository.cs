using Licensing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Licensing.IRepositories
{
    public interface IProductRepository
    {
        public Product addProduct(Product product);
        public List<Product> getProduct();
        public bool updateProduct(Product product);
        public bool deleteProduct(int id);
        public List<string> getProductsName();
    }
}
