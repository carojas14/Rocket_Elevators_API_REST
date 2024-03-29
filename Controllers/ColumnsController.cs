#nullable disable
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
    public class ColumnsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public ColumnsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumns()
        {
            return await _context.columns.ToListAsync();
        }

        // GET: api/Columns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> Get(int id)
        {
            var column = await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

        // PUT: api/Columns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult<Column>> Put(int id, string status)
        {
            var column = await _context.columns.FindAsync(id);

            if(column == null) {
                return NotFound();
            }
            // change status of battery
            column.status = status;
            _context.SaveChanges();

            return column;

        }


        // DELETE: api/Columns/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet("battery/{id}")]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumnByBattery(int id)
        {
            List<Column> columnsList = await _context.columns.ToListAsync();
            List<Column> filteredList = new List<Column>();
            filteredList = columnsList.Where(col => col.battery_id == id).ToList();

            if (filteredList == null)
            {
                return NotFound();
            }
            else
            {
                return filteredList;
            }
        }



        [Produces("application/json")]
        [HttpGet("customer/{email}")]
        public async Task<IActionResult> GetColumnByCustomer(string email)
        {


            var customer = await _context.customers.FirstOrDefaultAsync(cust => cust.EmailCompanyContact == email);

            /* var buildingsList = _context.buildings; */

            List<Battery> customerBatteries = new List<Battery>();
            List<Column> customerColumns = new List<Column>();


            var buildingList = _context.buildings.Where(build => build.customer_id == customer.id).ToList();

            foreach (var building in buildingList)
            {

                var batteries = _context.batteries.Where(batt => building.id == batt.building_id).ToList();
                customerBatteries.AddRange(batteries);
            }

            foreach (var battery in customerBatteries)
            {

                var columns = _context.columns.Where(col => battery.id == col.battery_id).ToList();
                customerColumns.AddRange(columns);
            }

            // Return a list of the customer's columns
            return Ok(customerColumns);
        }
    }
}
