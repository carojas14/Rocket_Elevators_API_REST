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
    public class ElevatorsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public ElevatorsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> Get(int id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }

        [HttpPut("{id}/status/{status}")]
        public async Task<ActionResult<Elevator>> Put(int id, string status)
        {
            // grab battery with id id
            var elevator = await _context.elevators.FindAsync(id);

            if(elevator == null) {
                return NotFound();
            }
            // change status of battery
            elevator.elevator_status = status;
            _context.SaveChanges();

            return elevator;
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorStat()
        {
            return await _context.elevators.Where(e=>(e.elevator_status == "Inactive")).ToListAsync();
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet("column/{id}")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorByColumn(int id)
        {
            List<Elevator> elevatorsList = await _context.elevators.ToListAsync();
            List<Elevator> filteredList = new List<Elevator>();
            filteredList = elevatorsList.Where(elev => elev.column_id == id).ToList();

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
        public async Task<IActionResult> GetElevatorByCustomer(string email)
        {

            var customer = await _context.customers.FirstOrDefaultAsync(cust => cust.EmailCompanyContact == email);

            // First get list of all buildings
            /* var buildingsList = _context.buildings; */

            List<Battery> customerBatteries = new List<Battery>();
            List<Column> customerColumns = new List<Column>();
            List<Elevator> customerElevators = new List<Elevator>();


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

            foreach (var columns in customerColumns)
            {

                var elevators = _context.elevators.Where(elev => columns.id == elev.column_id).ToList();
                customerElevators.AddRange(elevators);
            }


            return Ok(customerElevators);
        }
    }
}
