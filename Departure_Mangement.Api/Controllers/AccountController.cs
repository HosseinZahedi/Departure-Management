using Departure_Management.Application.Contracts.Identity;
using Departure_Management.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Departure_Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    public AccountController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authenticationService.Login(request));
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authenticationService.Register(request));
    }

    [HttpPost("ChangePassword")]
    [Authorize]
    public async Task<ActionResult<ChangePasswordResponse>> ChangePassword(ChangePasswordRequest request)
    {
        return Ok(await _authenticationService.ChangePassword(request));
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "Administrator")]
    public ActionResult<List<Employee>> GetAllUsers()
    {
        return Ok( _authenticationService.GetAllUsers());
    }

    [HttpGet("GetSingleUser/{id}")]
    public async Task<ActionResult<Employee>> GetSingleUser(string id)
    {
        return Ok(await _authenticationService.GetSingleUser(id));
    }

    [HttpPost("EditUser")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<bool>> EditUser(Employee employee)
    {
        return Ok(await _authenticationService.EditUser(employee));
    }
}