using System.Security.Principal;
using CourseApp.WebMVC.Models;
using CourseApp.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.WebMVC.Controllers;

[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private readonly IIdentityService _identity;
    public AuthController( IIdentityService identity)
    {
        _identity = identity;
    }

    public IActionResult Signin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signin(SigninInput input)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var response = await _identity.SingIn(input);

        if (!response.IsSuccesfull)
        {
            response.Errors.ForEach(e => 
                ModelState.AddModelError(String.Empty,e)   
            );
        return View();
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View("Error!");
    // }
}
