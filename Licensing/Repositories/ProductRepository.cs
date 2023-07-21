using Licensing.DBContext;
using Licensing.IRepositories;
using Licensing.Models;

namespace Licensing.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LDbContext _dbContext;
        public ProductRepository(LDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product addProduct(Product product)
        {
           if(product != null)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return product;
            }
           else
                return null;
        }
        public List<Product> getProduct()
        {
            return _dbContext.Products.ToList();
        }
        public bool updateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return true;
        }
        public bool deleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public List<string> getProductsName()
        {
            var products = _dbContext.Products.ToList();
            if(products.Count != 0)
            {
                var productId = new List<string>();
                foreach (Product product in products)
                {
                    productId.Add(product.Name);
                }
                return productId;
            }
            else
                return null;
        }

    }
}
