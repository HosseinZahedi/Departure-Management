namespace Departure_Management.Application.Models.Identity;

public class Employee
{
    public string? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; }
}