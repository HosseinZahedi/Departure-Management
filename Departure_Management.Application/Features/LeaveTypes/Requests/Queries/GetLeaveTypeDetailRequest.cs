using Departure_Management.Application.DTOs.LeaveType;
using MediatR;

namespace Departure_Management.Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDto>
{
    public int Id { get; set; }
}