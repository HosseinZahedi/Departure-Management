using Departure_Management.Application.DTOs.LeaveRequest;
using MediatR;

namespace Departure_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
{
    public bool IsLoggedInUser { get; set; }
}