using Departure_Management.MVC.Contracts;
using Departure_Management.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Departure_Management.MVC.Controllers;

public class UsersController : Controller
{
    private readonly IAuthenticationService _authService;

    public UsersController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM login, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authService.Authenticate(login.Email, login.Password);
            if (isLoggedIn)
                return LocalRedirect(returnUrl);
        }
        ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
        return View(login);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registration)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var isCreated = await _authService.Register(registration);
            if (isCreated)
                return LocalRedirect(returnUrl);
        }
            
        ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
        return View(registration);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> ManageUsers()
    {
        var result = await _authService.GetAllUsers();
        return View(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var result = await _authService.GetSingleUser(id);
        return View(result);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> EditUser(EmployeeVM request)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var isCreated = await _authService.EditUser(request);
            if (isCreated)
                return LocalRedirect(returnUrl);
        }

        ModelState.AddModelError("", "Editing The User Has Been Failed!");
        return View(request);
    }

    [Authorize]
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var isCreated = await _authService.ChangePassword(changePassword);
            if (isCreated)
                return LocalRedirect(returnUrl);
        }

        ModelState.AddModelError("", "Changing Password Attempt Failed. Please try again.");
        return View(changePassword);
    }

    [Authorize]
    public async Task<IActionResult> Logout(string returnUrl)
    {
        returnUrl ??= Url.Content("~/");
        await _authService.Logout();
        return LocalRedirect(returnUrl);
    }
}