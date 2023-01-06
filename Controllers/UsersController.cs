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
    public class UsersController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public UsersController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }


        [HttpGet("Email/{email}")]
        public async Task<ActionResult<Users>> GetUserEmail(string email)
        {
            IEnumerable<Users> usersAll = await _context.users.ToListAsync();

            foreach (Users user in usersAll)
            {
                if (user.email == email)
                {
                    return user;
                }
            }
            return NotFound();
        }

        // GET: api/Users/email
        [HttpGet("{email}")]
        public bool CheckEmail(string email)
        {
            return _context.users.Any(u => u.email == email);
        }

        private bool UsersExists(long id)
        {
            return _context.users.Any(e => e.id == id);
        }
    }
}
