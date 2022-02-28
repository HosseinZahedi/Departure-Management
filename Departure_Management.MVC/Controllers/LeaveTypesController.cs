using Departure_Management.MVC.Contracts;
using Departure_Management.MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Departure_Management.MVC.Controllers;

[Authorize]
public class LeaveTypesController : Controller
{
    private readonly ILeaveTypeService _leaveTypeService;
    private const string AuthSchemes =
        CookieAuthenticationDefaults.AuthenticationScheme + "," +
        JwtBearerDefaults.AuthenticationScheme;
    public LeaveTypesController(
        ILeaveTypeService leaveTypeService)
    {
        _leaveTypeService = leaveTypeService;
    }

    // GET: LeaveTypesController
    public async Task<ActionResult> Index()
    {
        var model = await _leaveTypeService.GetLeaveTypes();
        return View(model);
    }

    // GET: LeaveTypesController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var model = await _leaveTypeService.GetLeaveTypeDetails(id);

        return View(model);
    }

    [Authorize(Roles = "Administrator")]
    // GET: LeaveTypesController/Create
    public async Task<ActionResult> Create()
    {
        return View();
    }

    [Authorize(Roles = "Administrator")]
    // POST: LeaveTypesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveTypeVM leaveType)
    {
        try
        {
            var response = await _leaveTypeService.CreateLeaveType(leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View(leaveType);
    }

    [Authorize(Roles = "Administrator")]
    // GET: LeaveTypesController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var model = await _leaveTypeService.GetLeaveTypeDetails(id);

        return View(model);
    }

    [Authorize(Roles = "Administrator")]
    // POST: LeaveTypesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, LeaveTypeVM leaveType)
    {
        try
        {
            var response = await _leaveTypeService.UpdateLeaveType(id, leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View(leaveType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var response = await _leaveTypeService.DeleteLeaveType(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View();
    }
}