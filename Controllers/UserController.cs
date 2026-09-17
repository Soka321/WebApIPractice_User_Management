using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApIPractice.Data;
using WebApIPractice.Models;
using WebApIPractice.ViewModel;

namespace WebApIPractice.Controllers
{
    public class UserController : Controller
    {

        private IMemoryCache _Cache;
        private ApiContext _context;
        private ILogger _logger;
        private readonly IConfiguration _configuration;

        public UserController(IMemoryCache Cache,
         ApiContext context,
         ILogger logger, IConfiguration configuration)
        {
            _Cache = Cache;
            _context = context;
            _logger = logger;
            _configuration = configuration;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginVM log)
        {
            try
            {
                if (log.Email != "Soka@gmail.com" && log.Password != "321")
                {
                    return NotFound("User Not Found!!");
                }
                var token = GenerateToken(log.Email);
                var RefreshToken = GenerateRefreshToken();

                return Ok(new { token, RefreshToken });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private string GenerateToken(string Email)
        {
            var claims = new Claim[] { new Claim(ClaimTypes.Email, Email) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
            expires: DateTime.Now.AddMinutes(30),

            signingCredentials: creds


            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var RandomNumber = new Byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);

        }
        //private List<User> users;
        [HttpGet("Users")]
        public ActionResult GetUsers()
        {
            try
            {

                if (_Cache.TryGetValue("Users", out List<User>? users))
                {

                    return Ok(users);
                }

                users = _context.users.ToList();

                if (User == null)
                {
                    return NotFound("No users found");
                }
                _Cache.Set("Users", users);
                return Ok(users);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddUser")]
        public ActionResult AddUser(User user)
        {
            try
            {
                if (user != null)
                {
                    return Conflict("User already exists");
                }
                var response = _context.users.Add(user);
                _context.SaveChanges();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }

        }
    }
    }
