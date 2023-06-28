using Licensing.IRepositories;
using Licensing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Licensing.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public IActionResult Createuser([FromBody] User user)
        {
            var newuser = _userRepository.createUser(user);
            if (newuser != null)
            {
                return Ok(new { message = "User Created Successfully" });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            var updateuser = _userRepository.updateUser(user);
            if(updateuser == true)
                return Ok(new { message = "User Updated Successfully"});
            else
                return BadRequest("User not Updated");
        }

        [HttpGet]
        public IActionResult GetUserDetails()
        {
            var user = _userRepository.getUserDetails().ToList();
            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUserList()
        {
            var users = _userRepository.getUserList();
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound("No User Found");
            }
        }
    }
}
