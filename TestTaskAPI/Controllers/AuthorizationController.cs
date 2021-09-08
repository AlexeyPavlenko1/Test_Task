using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestTaskAPI.Data;

namespace TestTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UsersContext _db;
        private readonly IJwtAuthenticationManager _jwtManager;
        public AuthorizationController(UsersContext context, IJwtAuthenticationManager jwtManager)
        {
            _db = context;
            _jwtManager = jwtManager;
        }


        /// <summary>
        /// Creates a new user and adding it to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns>token or BadRequest</returns>
        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            User created = new User { Name = user.Name, Login = user.Login, Password = hashedPassword };

            await _db.Users.AddAsync(created);
            await _db.SaveChangesAsync();

            string token = _jwtManager.GetToken(created.Login, created.Password);

            return Ok(token);
        }

        /// <summary>
        /// Authentication by login and password
        /// </summary>
        /// <param name="creds"></param>
        /// <example>{"name": "string","login": "string"}</example>
        /// <returns>token or unauthorized</returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredsModel creds)
        {
            string token = _jwtManager.Authenticate(_db, creds.Login, creds.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
