using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MachineTestRebin.ContextFile;
using MachineTestRebin.Models;

namespace MachineTestRebin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, AddUser user)
        {
            var exuser = await _context.Users.FindAsync(id);
            if (exuser != null)
            {
                exuser.Username= user.Username;
                exuser.Gender= user.Gender;
                exuser.Birthday = DateTime.ParseExact(user.Birthday, "yyyy-MM-dd", null);
                exuser.Status= user.Status;
                exuser.Address= user.Address;
                await _context.SaveChangesAsync();
                return Ok(exuser);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(AddUser adduser)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationContext.Users'  is null.");
            }
            var currentTime = DateTime.Now;
            User user=new User();
            user.Username = adduser.Username;
            user.Gender = adduser.Gender;
            user.Birthday = DateTime.ParseExact(adduser.Birthday, "yyyy-MM-dd", null);
            user.Status = adduser.Status;
            user.Address = adduser.Address;
            user.Time = currentTime.ToString("hh:mm tt");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
