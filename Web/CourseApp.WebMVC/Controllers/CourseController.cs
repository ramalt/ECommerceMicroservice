using CourseApp.Shared.services;
using CourseApp.WebMVC.Models.Catalog;
using CourseApp.WebMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseApp.WebMVC.Controllers;

[Authorize]
[Route("[controller]/[action]")]
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

    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoriesAsync();

        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseInput courseInput)
    {
        var categories = await _catalogService.GetAllCategoriesAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        if (!ModelState.IsValid)
        {
            return View();
        }
        courseInput.UserId = _identityService.GetUserId;

        await _catalogService.CreateCourseAsync(courseInput);

        return RedirectToAction(nameof(Index));
    }
}
