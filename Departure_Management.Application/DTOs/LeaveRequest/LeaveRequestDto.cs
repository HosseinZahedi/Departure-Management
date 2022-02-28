using Departure_Management.Application.DTOs.Common;
using Departure_Management.Application.DTOs.LeaveType;
using Departure_Management.Application.Models.Identity;

namespace Departure_Management.Application.DTOs.LeaveRequest;

public class LeaveRequestDto : BaseDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Employee Employee { get; set; }
    public string RequestingEmployeeId { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
}