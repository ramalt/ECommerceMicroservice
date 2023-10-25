using CourseApp.Shared.services;
using CourseApp.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.WebMVC.Controllers;

[Route("[controller]")]
public class CourseController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentityService _identityService;

    public CourseController(ICatalogService service, ISharedIdentityService identityService)
    {
        _catalogService = service;
        _identityService = identityService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _catalogService.GetAllCourseByIdAsync(_identityService.GetUserId));
    }

}
