using Departure_Management.Application.DTOs.Common;

namespace Departure_Management.Application.DTOs.LeaveType;

public class LeaveTypeDto : BaseDto, ILeaveTypeDto
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}