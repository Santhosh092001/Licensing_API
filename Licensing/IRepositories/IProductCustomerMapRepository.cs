using Licensing.Dto;
using Licensing.Models;

namespace Licensing.IRepositories
{
    public interface IProductCustomerMapRepository
    {
        public List<ProductCustomerMap> get();
        public ProductCustomerMap createProductCustomerMap(ProductCustomerMap newproductCustomerMap);
        public bool updateProductCustomerMap(ProductCustomerMap updateMap);
        public List<int> GetProductCustomersId();


        /*---------------Join Linq Query----------------*/
        public List<ProductCustomerJoinDto> gets();
    }
}
