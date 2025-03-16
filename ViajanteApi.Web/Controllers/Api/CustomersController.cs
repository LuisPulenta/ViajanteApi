using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ViajanteApi.Web.Data;
using ViajanteApi.Web.Data.Entities;
using System.Linq;


namespace ViajanteApi.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;
        
        public CustomersController(DataContext context)
        {
            _context = context;
        }

        //-----------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            List<Customer> customers = await _context.Customers
              .OrderBy(x => x.Name)
              .ToListAsync();
            return Ok(customers);
        }
        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("GetCustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            List<Customer> customers = await _context.Customers
              .OrderBy(x => x.Name)
              .ToListAsync();
            return Ok(customers);
        }
        //-----------------------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer oldCustomer = await _context.Customers.FirstOrDefaultAsync(o => o.Id == customer.Id);            oldCustomer!.Name = customer.Name;
            
            _context.Update(oldCustomer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya existe un cliente con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok(oldCustomer);
        }
        
        //-----------------------------------------------------------------------------------
        [HttpPost]
        [Route("PostCustomer")]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicada"))
                {
                    return BadRequest("Ya existe este Cliente.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        //-----------------------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        //-----------------------------------------------------------------------------------
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            List<Customer> customers = await _context.Customers
             .OrderBy(x => x.Name)
             .ToListAsync();

            return Ok(customers);
        }

        //-----------------------------------------------------------------------------------

        [HttpPost]
        [Route("GetCustomerById/{id}")]
        public async Task<ActionResult<Customer>> GetCategoryById(int id)
        {

            Customer customer = await _context.Customers
                .FirstOrDefaultAsync(p => p.Id == id);


            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
