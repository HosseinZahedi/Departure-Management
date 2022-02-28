using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Departure_Management.Application.Constants;
using Departure_Management.Application.Contracts.Identity;
using Departure_Management.Application.Models.Identity;
using Departure_Management.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Departure_Management.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var role = await _userManager.GetRolesAsync(user);
        if (user == null)
        {
            throw new Exception($"User with {request.Email} not found.");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new Exception($"Credentials for '{request.Email} aren't valid'.");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Email);

        if (existingUser != null)
        {
            throw new Exception($"Username '{request.Email}' already exists.");
        }

        var user = new ApplicationUser();
        await _userManager.SetUserNameAsync(user, request.Email);
        await _userManager.SetEmailAsync(user, request.Email);
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;

        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                throw new Exception($"{result.Errors}");
            }
        }
        else
        {
            throw new Exception($"Email {request.Email } already exists.");
        }
    }

    [Authorize]
    public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
            q => q.Type == CustomClaimTypes.Uid)?.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User not found.");
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new Exception($"Credentials that you provided aren't valid'.");
        }

        return new ChangePasswordResponse() {UserId = userId};
    }

    public List<Employee> GetAllUsers()
    {
        
        var employees = _userManager.Users;
        return employees.Select(q => new Employee
        {
            Id = q.Id,
            Email = q.Email,
            FirstName = q.FirstName,
            LastName = q.LastName,
            Role = _userManager.IsInRoleAsync(q, "Employee").Result ? "Employee" : "Administrator"
        }).ToList();
    }

    public async Task<Employee> GetSingleUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var role = await _userManager.GetRolesAsync(user);
        return new Employee()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = role[0]
        };
    }

    public async Task<bool> EditUser(Employee employee)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
            q => q.Type == CustomClaimTypes.Uid)?.Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User not found.");
        }

        user.FirstName = employee.FirstName;
        user.LastName = employee.LastName;
        user.Email = employee.Email;
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, employee.Password);

        var currentRole = await _userManager.GetRolesAsync(user);
        if (currentRole[0] != employee.Role)
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(user, currentRole[0]);
            var addResult = await _userManager.AddToRoleAsync(user, employee.Role);

            if (!removeResult.Succeeded || !addResult.Succeeded)
            {
                throw new Exception("Adding Role Failed");
            }
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception($"Updating Failed");
        }
        return true;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (int i = 0; i < roles.Count; i++)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        }

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}