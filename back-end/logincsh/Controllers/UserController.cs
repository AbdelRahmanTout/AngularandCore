using logincsh.Context;
using logincsh.Helpers;
using logincsh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace logincsh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }
        [HttpPost("authentificate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObject)
        {
            if (userObject == null)
            {
                return BadRequest();
            }

            var user = await _authContext.Users.FirstOrDefaultAsync
                (x => x.UserName == userObject.UserName);

            if (user == null)
            {
                return NotFound(new { Message = "User Not Found!" });
            }

            if (passwordHasher.verifyPassword(userObject.Password, user.Password))
            {
                return BadRequest(new { Message = "password is incorrect" });
            }
            return Ok(new
            {
                Message = "Login success!"
            });
        }
        //HTTPPost save the data and send it to database
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            if (await CheckUserName(userObj.UserName))
            {
                return BadRequest(new { Message = "Username already exist" });
            }

            if (await CheckEmail(userObj.Email))
            {
                return BadRequest(new { Message = "Email already exist" });
            }
            var pass = CheckPasswordStrength(userObj.Password);
            if (string.IsNullOrEmpty(pass)){ 
                return BadRequest(new { Message = pass }); }
             
            userObj.Password = passwordHasher.HashPassword(userObj.Password);
            userObj.Role = "User";
            userObj.Token = " ";
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Register"
            });
        }

        private Task<bool> CheckUserName(string username) =>
        _authContext.Users.AnyAsync(x => x.UserName == username);

        private Task<bool> CheckEmail(string email) =>
       _authContext.Users.AnyAsync(x => x.Email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
            {
                sb.Append("Minimum password length should be 8 " + Environment.NewLine);
            }
            if ((Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]"))) ;
            sb.Append("password should be Alpha numerique" + Environment.NewLine);

            if (!Regex.IsMatch(password,"[<,>,@,#,$,%]"))
                    sb.Append("password should contain specail char");
            return sb.ToString();
        }
    }
}
