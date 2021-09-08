using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskAPI.Data;

namespace TestTaskAPI.Controllers
{
    /// <summary>
    /// Controller for CRUD operations with users in DB
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _db;

        public UsersController(UsersContext context)
        {
            _db = context;
        }

        // GET: api/Users
        /// <summary>
        /// Returns names and logins of all registred users
        /// </summary>
        /// <returns>List of all users in DB</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserNameLoginModel>>> GetAllUsers()
        {
            List<User> users = await _db.Users.ToListAsync();
            List<UserNameLoginModel> result = new List<UserNameLoginModel>();
            foreach (User user in users)
            {
                result.Add(new UserNameLoginModel { Name = user.Name, Login = user.Login });
            }
            return result;
        }

        /// <summary>
        /// Returns specific user by his Id in DB
        /// </summary>
        /// <param name="id">Id of user in DB</param>
        /// <returns>Name and login of specified user or NotFound</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserNameLoginModel>> GetUser(int id)
        {
            User user = await _db.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            UserNameLoginModel nameLogin = new UserNameLoginModel { Name = user.Name, Login = user.Login };

            return nameLogin;
        }

        // PUT: api/Users/5
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>updated user</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!_db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            User modified = new User
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            _db.Update(modified);
            await _db.SaveChangesAsync();
            return Ok(user);

        }


        // DELETE: api/Users/5
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NotFound or NoContent, if succsesfully deleted</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
