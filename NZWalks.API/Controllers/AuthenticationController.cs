using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository.IRepository;
using System.Data;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        // POST: /api/Authentication/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.UserName
            };

            // register user and hashed password
            var identityResult = await userManager.CreateAsync(identityUser, registerDTO.Password);
            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (registerDTO.Roles != null && registerDTO.Roles.Any())
                {
                    foreach (var role in registerDTO.Roles)
                    {
                        identityResult = await userManager.AddToRoleAsync(identityUser, role);
                    }
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }

            }
            return BadRequest("Something went wrong");
        }


        // POST: /api/Authentication/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // check if user regisered or not
           var user = await userManager.FindByEmailAsync(loginDTO.UserName);

            if (user != null)
            {
                // check password is matched or not
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginDTO.Password);  
                if (checkPasswordResult)
                {
                    //Get Roles for this user
                    var Roles = await userManager.GetRolesAsync(user);
                    if (Roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, Roles.ToList());
                        return Ok( new
                        {
                            token = jwtToken,
                            expiration = DateTime.Now.AddMinutes(15),
                            username = user.UserName,
                            roles = Roles
                        });
                    }
                }
            }
            return BadRequest("UserName or Password incorrect");
        }
        

    }
}
