using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Blogs.Models;

namespace WebApplication_Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly BlogDBContext _context = null;

        public AuthController(IConfiguration config,BlogDBContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User LoginPrams)
        {
            var user = AuthenticateUser(LoginPrams);
            if(user != null)
            {
                var jwt_token = GenerateJwt(user);
                return Ok(new { token = jwt_token });
            }
            return Unauthorized();
        }

        private User AuthenticateUser(User LoginPrams)
        {
            var user = _context.Users.SingleOrDefault(
                u => u.Username == LoginPrams.Username &&
                u.Password == LoginPrams.Password
                );
            if(user != null)
                return user;
            
            return null; 
        }
        private string GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,  SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
