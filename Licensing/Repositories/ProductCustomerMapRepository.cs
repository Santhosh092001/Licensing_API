using Licensing.DBContext;
using Licensing.Dto;
using Licensing.IRepositories;
using Licensing.Models;

namespace Licensing.Repositories
{
    public class ProductCustomerMapRepository : IProductCustomerMapRepository
    {
        private readonly LDbContext _dbContext;
        public ProductCustomerMapRepository(LDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public List<ProductCustomerMap> get()
        {
            return _dbContext.ProductCustomerMaps.ToList();
        }

        public ProductCustomerMap createProductCustomerMap(ProductCustomerMap productCustomerMap)
        {
            productCustomerMap.Product = null;
            productCustomerMap.Customer = null;
            _dbContext.ProductCustomerMaps.Add(productCustomerMap);

            _dbContext.SaveChanges();
            return productCustomerMap;
        }


        public bool updateProductCustomerMap(ProductCustomerMap updateMap)
        {
            _dbContext.ProductCustomerMaps.Update(updateMap);
            _dbContext.SaveChanges();
            return true;
        }

        public List<int> GetProductCustomersId()
        {
            var getproductCustomersId = _dbContext.ProductCustomerMaps.ToList();
            if(getproductCustomersId != null)
            {
                var productCustomersId = new List<int>();
                foreach(ProductCustomerMap productCustomer in getproductCustomersId)
                {
                    productCustomersId.Add(productCustomer.Id);
                }
                return productCustomersId;
            }
            else
            {
                return null;
            }
        }



        /*---------------Join Linq Query----------------*/

        public List<ProductCustomerJoinDto> gets()
        {
            var lists = new List<ProductCustomerJoinDto>();
            var q = (from products in _dbContext.Products
                     join productCustomer in _dbContext.ProductCustomerMaps on products.Id equals productCustomer.ProductId
                     join customers in _dbContext.Customers on productCustomer.CustomerId equals customers.Id
                     select new
                     {
                         productCustomer.Id,
                         products.Name,
                         CustomerName = customers.Name,
                     }).ToList();
            foreach(var qs in q)
            {
                /*list.ProductId = qs.Id;
                list.ProductName = qs.Name;
                list.CustomerName = qs.CustomerName;
                lists.Add(list);*/
                lists.Add(new ProductCustomerJoinDto()
                {

                    ProductId = qs.Id,
                    ProductName = qs.Name,
                    CustomerName = qs.CustomerName
                });     
            }       
            return lists;
        }

    }
}
