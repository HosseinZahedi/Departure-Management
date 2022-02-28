using Departure_Management.Application.DTOs.LeaveType;
using Departure_Management.Application.Responses;
using MediatR;

namespace Departure_Management.Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; }
}