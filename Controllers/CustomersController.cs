using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Rest_API.Models;

namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public CustomersController(RocketElevatorsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            return await _context.customers.ToListAsync();
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            var customer = await _context.customers.Where(c => c.EmailCompanyContact == email).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }


    }
}
