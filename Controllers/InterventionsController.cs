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
    public class InterventionsController : ControllerBase
    {

        private readonly RocketElevatorsContext _context;

        public InterventionsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventions()
        {
            if (_context.interventions == null)
            {
                return NotFound();
            }
            return await _context.interventions.ToListAsync();
        }

        // GET api/Interventions/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // Find all intervention that do not have a start date and are in "Pending" status.
        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventionStatus()
        {

            List<Intervention> interventions = await _context.interventions.ToListAsync();
            List<Intervention> interventionList = new List<Intervention>();

            foreach (Intervention intervention in interventions)
            {
                if (intervention.StartDate == null && intervention.Status == "Pending")
                {
                    interventionList.Add(intervention);
                }
            }

            return interventionList;

        }


        // Set the status to "InProgress" and add a start date and time
        [HttpPut("{id}/inProgress")]
        public async Task<ActionResult<Intervention>> SetInterventionStatus(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }
            var date = DateTime.Now;

            intervention.StartDate = date;
            intervention.Status = "InProgress";

            _context.interventions.Update(intervention);
            _context.SaveChanges();

            return intervention;
        }

        // Set the status to "Completed" and add an end date and time
        [HttpPut("{id}/completed")]
        public async Task<ActionResult<Intervention>> SetInterventionCompleted(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            intervention.EndDate =  DateTime.Now;
            intervention.Status = "Completed";

            _context.interventions.Update(intervention);
            _context.SaveChanges();

            return intervention;
        }

         [HttpGet("customer/{email}")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventionByCustomer(string email)
        {

            var customer = await _context.customers.Where(c => c.EmailCompanyContact == email).FirstOrDefaultAsync();
            return await _context.interventions.Where(i => i.CustomerID == customer.id).ToListAsync();

        }


        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            intervention.Status = "InProgress";
            intervention.Result = "Incomplete";
            intervention.EmployeeID = null;
            intervention.StartDate = DateTime.Now;

            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return intervention;
        }


        // PUT api/<InterventionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InterventionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
