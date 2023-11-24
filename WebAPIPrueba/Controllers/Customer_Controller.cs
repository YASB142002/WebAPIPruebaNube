using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIPruebaData.Repository;
using WebAPIPruebaModels;

namespace WebAPIPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer_Controller : ControllerBase
    {
        private readonly ICustomerRepository _CustomerRepository;

        public Customer_Controller (ICustomerRepository customerRepository)
        {
            _CustomerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers() 
        { 
            return Ok(await _CustomerRepository.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDetails(int id)
        {
            return Ok(await _CustomerRepository.GetCustomerDetails(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _CustomerRepository.InsertCustomer(customer);
            return Created("Created", created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _CustomerRepository.UpdateCustomer(customer);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _CustomerRepository.DeleteCustomer(id);
            return NoContent();
        }
    }
}
