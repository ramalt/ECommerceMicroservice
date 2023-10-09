using System.Threading.Tasks;
using CourseApp.IdentityServer.Dtos;
using CourseApp.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CourseApp.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using static IdentityServer4.IdentityServerConstants;

namespace CourseApp.IdentityServer.Controller
{
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(400, result.Errors.Select(x => x.Description).ToList()));
            }

            return NoContent();
        }

        //! TODO: claims okunöuyor. istek at dene.
        [HttpGet] 
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub); //? OpenId, Email, Profile, roles

            if (userIdClaim == null) return BadRequest(new { error = "claims not found" });

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null) return BadRequest(new { error = "User not found" });

            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email });
        }
    }
}