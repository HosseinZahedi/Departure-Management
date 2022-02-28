namespace Departure_Management.Application.DTOs.LeaveType;

public class CreateLeaveTypeDto : ILeaveTypeDto
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}