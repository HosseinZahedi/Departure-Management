using MediatR;

namespace Departure_Management.Application.Features.LeaveRequests.Requests.Commands;

public class DeleteLeaveRequestCommand : IRequest
{
    public int Id { get; set; }
}