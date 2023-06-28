using Licensing.DBContext;
using Licensing.Dto;
using Licensing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Licensing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly LDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthenticationController(LDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        private string GenerateJwtToken(User authuser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var credential = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                new Claim("Username", authuser.UserName),
                new Claim("FirstName", authuser.FirstName),
                new Claim("LastName", authuser.LastName),
                    }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Auidence"],
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private User Authentication(UserDto authuser)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == authuser.UserName);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public IActionResult AuthenticateUser([FromBody] UserDto authuser)
        {
            IActionResult response = Unauthorized();
            var user = Authentication(authuser);
            if (user != null)
            {
                if (user.Password != authuser.Password)
                {
                    response = NotFound("Password is Incorrect");
                    return response;
                }
                else
                {
                    var token = GenerateJwtToken(user);
                    response = Ok(new { Token = token, message = "Login Successfully" });
                    return response;
                }
            }
            else
            {
                response = NotFound("User not Found");
                return response;
            }
        }
    }
}
