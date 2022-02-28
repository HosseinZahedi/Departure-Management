using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Departure_Management.MVC.Contracts;
using Departure_Management.MVC.Models;
using Departure_Management.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using IAuthenticationService = Departure_Management.MVC.Contracts.IAuthenticationService;
using IClient = Departure_Management.MVC.Services.Base.IClient;

namespace Departure_Management.MVC.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private JwtSecurityTokenHandler _tokenHandler;

    public AuthenticationService(IClient client, ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
        : base(client, localStorage)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        try
        {
            AuthRequest authenticationRequest = new() { Email = email, Password = password };
            var authenticationResponse = await _client.LoginAsync(authenticationRequest);
            if (authenticationResponse.Token != string.Empty)
            {
                //Get Claims from token and Build auth user object
                var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorage.SetStorageValue("token", authenticationResponse.Token);

                return true;
            }
            return false;
        }
        catch 
        {
            return false;
        }
    }

    public async Task<bool> Register(RegisterVM registration)
    {

        RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);
        var response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            await Authenticate(registration.Email, registration.Password);
            return true;
        }
        return false;
    }

    public async Task<bool> ChangePassword(ChangePasswordVM changePassword)
    {
        ChangePasswordRequest changePasswordRequest = _mapper.Map<ChangePasswordRequest>(changePassword);
        AddBearerToken();
        var response = await _client.ChangePasswordAsync(changePasswordRequest);

        return !string.IsNullOrEmpty(response.UserId);
    }

    public async Task<List<EmployeeVM>> GetAllUsers()
    {
        AddBearerToken();
        var response = await _client.GetAllUsersAsync();
        var result = new List<EmployeeVM>();
        foreach (var employee in response)
        {
            result.Add(_mapper.Map<EmployeeVM>(employee));
        }
        return result;
    }

    public async Task<EmployeeVM> GetSingleUser(string id)
    {
        var response = await _client.GetSingleUserAsync(id);
        var user = _mapper.Map<EmployeeVM>(response);
        return user;
    }

    public async Task<bool> EditUser(EmployeeVM request)
    {
        AddBearerToken();
        Employee employee= _mapper.Map<Employee>(request);
        var response = await _client.EditUserAsync(employee);
        return response;
    }

    public async Task Logout()
    {
        _localStorage.ClearStorage(new List<string> { "token" });
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
    {
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}