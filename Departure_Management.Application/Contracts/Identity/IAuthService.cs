using Departure_Management.Application.Models.Identity;

namespace Departure_Management.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
    Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
    Task<List<Employee>> GetAllUsers();
    Task<Employee> GetSingleUser(string userId);
    Task<bool> EditUser(Employee employee);
}