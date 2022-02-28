using Departure_Management.Application.DTOs.LeaveType;
using MediatR;

namespace Departure_Management.Application.Features.LeaveTypes.Requests.Commands;

public class UpdateLeaveTypeCommand : IRequest<Unit>
{
    public LeaveTypeDto LeaveTypeDto { get; set; }

}