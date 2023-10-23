using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace CourseApp.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByEmailAsync(context.UserName);

            if (user == null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string>{"Email veya Parola hatalı."});
                context.Result.CustomResponse = errors;
                return;
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(user, context.Password);


            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string>{"Email veya Parola hatalı."});
                context.Result.CustomResponse = errors;
                return;
            }
            context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);


        }
    }
}