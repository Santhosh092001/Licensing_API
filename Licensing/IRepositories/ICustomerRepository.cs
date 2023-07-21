using Licensing.Models;

namespace Licensing.IRepositories
{
    public interface ICustomerRepository
    {
        public List<Customer> getCustomer();
        public Customer addCustomer(Customer customer);
        public bool updateCustomer(Customer customer);
        public bool deleteCustomer(int id);
        public List<string> getCustomersName();
    }
}
