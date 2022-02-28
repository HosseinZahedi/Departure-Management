using Departure_Management.MVC.Models;

namespace Departure_Management.MVC.Contracts;

public interface IAuthenticationService
{
    Task<bool> Authenticate(string email, string password);
    Task<bool> Register(RegisterVM registration);
    Task<bool> ChangePassword(ChangePasswordVM changePassword);
    Task<List<EmployeeVM>> GetAllUsers();
    Task<EmployeeVM> GetSingleUser(string id);
    Task<bool> EditUser(EmployeeVM request);
    Task Logout();
}