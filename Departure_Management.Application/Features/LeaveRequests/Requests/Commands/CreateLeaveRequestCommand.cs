using Departure_Management.Application.DTOs.LeaveRequest;
using Departure_Management.Application.Responses;
using MediatR;

namespace Departure_Management.Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveRequestCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveRequestDto LeaveRequestDto { get; set; }

}