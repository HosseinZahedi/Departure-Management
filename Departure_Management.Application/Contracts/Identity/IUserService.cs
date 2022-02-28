using Departure_Management.Application.Models.Identity;

namespace Departure_Management.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
}