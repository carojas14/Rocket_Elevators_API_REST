using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Rest_API.Models;
#nullable disable

namespace Rocket_Elevators_Rest_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public BatteriesController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/<Batteries>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET api/Batteries/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> Get(int id)
        {
            var battery = await _context.batteries.FindAsync(id);
            if(battery == null) return NotFound();
            return battery;
        }

        // PUT api/batteries/id/status/status
        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult<Battery>> Put(int id, string status)
        {
            // grab battery with id id
            var battery = await _context.batteries.FindAsync(id);

            if(battery == null) {
                return NotFound();
            }
            // change status of battery
            battery.status = status;
            _context.SaveChanges();

            return battery;
        }

        // DELETE api/<BatteriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("building/{id}")]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteriesByBuild(int id)
        {
            List<Battery> batteriesList = await _context.batteries.ToListAsync();
            List<Battery> filteredList = new List<Battery>();
            filteredList = batteriesList.Where(battery => battery.building_id == id).ToList();

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
        public async Task<IActionResult> GetBatteryByCustomer(string email)
        {


            var customer = await _context.customers.FirstOrDefaultAsync(cust => cust.EmailCompanyContact == email);


            /* var buildingsList =_context.buildings; */

            List<Battery> customerBatteries = new List<Battery>();


            var buildingList =_context.buildings.Where(build => build.customer_id == customer.id).ToList();

            foreach (var building in buildingList)
            {

                var batteries = _context.batteries.Where(batt => building.id == batt.building_id).ToList();
                customerBatteries.AddRange(batteries);
            }


            return Ok(customerBatteries);
        }



    }
}
