using Licensing.DBContext;
using Licensing.IRepositories;
using Licensing.Models;

namespace Licensing.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LDbContext _dbContext;
        public CustomerRepository(LDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Customer> getCustomer()
        {
            return _dbContext.Customers.ToList();
        }
        public Customer addCustomer(Customer customer)
        {
            if(customer != null)
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return customer;
            }
            else
                return null;
        }
        public bool updateCustomer(Customer customer)
        {
            if(customer != null)
            {
                _dbContext.Customers.Update(customer);
                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool deleteCustomer(int id)
        {
            var customerId = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
            if(customerId != null)
            {
                _dbContext.Customers.Remove(customerId);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<string> getCustomersName()
        {
            var customersId = _dbContext.Customers.ToList();
            if (customersId.Count != 0)
            {
                var customers = new List<string>();
                foreach (Customer customer in customersId)
                {
                    customers.Add(customer.Name);
                }
                return customers;
            }
            else
                return null;
        }

    }
}
