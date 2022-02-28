using System.Text.Json.Nodes;
using Departure_Management.MVC.Contracts;
using Departure_Management.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Dynamic.Core;
using JsonObject = System.Text.Json.Nodes.JsonObject;

namespace Departure_Management.MVC.Controllers;

[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly ILeaveTypeService _leaveTypeService;
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestsController(
        ILeaveTypeService leaveTypeService, 
        ILeaveRequestService leaveRequestService)
    {
        _leaveTypeService = leaveTypeService;
        _leaveRequestService = leaveRequestService;
    }

    // GET: LeaveRequest/Create
    public async Task<ActionResult> Create()
    {
        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");


        var model = new CreateLeaveRequestVM
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            LeaveTypes = leaveTypeItems
        };
        return View(model);
    }

    // POST: LeaveRequest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
    {
        if (ModelState.IsValid)
        {
            var response = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }

        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
        leaveRequest.LeaveTypes = leaveTypeItems;

        return View(leaveRequest);
    }

    // GET: LeaveRequest
    public async Task<ActionResult> Index()
    {
        var model = await _leaveRequestService.GetAdminLeaveRequestList();
        return View(model);
    }

    public async Task<ActionResult> Details(int id)
    {
        var model = await _leaveRequestService.GetLeaveRequest(id);
        return View(model);
    }

    public async Task<string> GetAllLeaves(bool pending, bool accepted, bool rejected)
    {
        var model = await _leaveRequestService.GetAdminLeaveRequestList();
        var allLeaveRequests = model.LeaveRequests.AsQueryable();

        var condition = "p => ";
        if (pending)
            condition += "p.Approved == null || ";
        if (accepted)
            condition += "p.Approved == true || ";
        if (rejected)
            condition += "p.Approved == false || ";
        condition += "1 == 0";

        var selectedLeaves = allLeaveRequests.Where(condition);
        var jsonArray = new JsonArray();

        foreach (var item in selectedLeaves)
        {
            var jsonItem = new JsonObject();
            jsonItem.Add("title", item.Employee.FirstName + " " + item.Employee.LastName);
            jsonItem.Add("start", item.StartDate.ToString("yyyy-MM-dd"));
            jsonItem.Add("end", item.EndDate.ToString("yyyy-MM-dd"));
            if (item.Approved == true)
            {
                jsonItem.Add("color", "green");
            }
            else if(item.Approved == false)
            {
                jsonItem.Add("color", "red");
            }
            else
            {
                jsonItem.Add("color", "#0878af");
            }
            jsonArray.Add(jsonItem);
        }

        return jsonArray.ToString();
    }

    [HttpGet]
    public async Task<ActionResult> MyLeaves()
    {
        var model = await _leaveRequestService.GetUserLeaveRequests();
        return View(model);
    }

    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> ApproveRequest(int requestId, bool? approved)
    {
        try
        {
            await _leaveRequestService.ApproveLeaveRequest(requestId, approved);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
