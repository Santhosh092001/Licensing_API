using Licensing.IRepositories;
using Licensing.Models;
using Microsoft.AspNetCore.Mvc;

namespace Licensing.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var customers = _customerRepository.getCustomer();
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            var newcustomer = _customerRepository.addCustomer(customer);
            if(newcustomer != null)
                return Ok(new { message = "Customer Created Successfully"});
            else
                return BadRequest("Customer not Created");
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer updateCustomer)
        {
            var customer = _customerRepository.updateCustomer(updateCustomer);
            if (customer == true)
                return Ok(new { message = "Customer Updated Successfully"});
            else
                return BadRequest("Customer not Updated");
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            _customerRepository.deleteCustomer(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult getCustomersName()
        {
            var customers = _customerRepository.getCustomersName();
            return Ok(customers);
        }

    }
}
