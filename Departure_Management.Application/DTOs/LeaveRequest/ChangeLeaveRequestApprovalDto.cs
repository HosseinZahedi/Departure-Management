using Departure_Management.Application.DTOs.Common;

namespace Departure_Management.Application.DTOs.LeaveRequest;

public class ChangeLeaveRequestApprovalDto : BaseDto
{
    public bool? Approved { get; set; }
}