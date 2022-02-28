using Departure_Management.Application.DTOs.LeaveRequest;
using MediatR;

namespace Departure_Management.Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public UpdateLeaveRequestDto LeaveRequestDto { get; set; }

    public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }

}