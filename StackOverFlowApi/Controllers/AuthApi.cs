using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackOverFlowApi.Data.Tables;
using StackOverFlowApi.ViewsModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlowApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthApi : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration config;
        private readonly SignInManager<User> signInManager;

        public AuthApi(UserManager<User> userManager, IConfiguration _config, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            config = _config;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            IActionResult response = Unauthorized();
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user != null &&  await userManager.CheckPasswordAsync(user, login.Password))
            { 
                var tokenString = GenerateJSONWebToken(user);
                await signInManager.SignInAsync(user, isPersistent:false);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        [HttpPost("Logout")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { Logout = "done" });
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                                new Claim("DateOfJoing", userInfo.RegisterDate.ToString("yyyy-MM-dd")),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
