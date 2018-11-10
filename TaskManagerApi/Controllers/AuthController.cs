using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("gettoken")]
        [HttpPost]
        public IActionResult GetToken()
        {
            var header = Request.Headers["Authorization"];

            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernamePass = Encoding.UTF8.GetString(Convert.FromBase64String(credValue)).Split(":");

                if (usernamePass[0] == "Admin" && usernamePass[1] == "pass")
                {
                    var claim = new[] { new Claim(ClaimTypes.Name, usernamePass[0]) };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("SecurityKey").Value));
                    var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken(
                        issuer: "taskmanagerapi",
                        audience: "taskmanagerapi",
                        expires: DateTime.Now.AddDays(1),
                        claims: claim,
                        signingCredentials: signInCredentials
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
            }
            return BadRequest("Wrong Request");
        }
    }
}