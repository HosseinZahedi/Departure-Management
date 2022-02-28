using AutoMapper;
using Departure_Management.Application.Constants;
using Departure_Management.Application.Contracts.Identity;
using Departure_Management.Application.Contracts.Persistence;
using Departure_Management.Application.DTOs.LeaveRequest;
using Departure_Management.Application.Features.LeaveRequests.Requests.Queries;
using Departure_Management.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Departure_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        var leaveRequests = new List<LeaveRequest>();
        var requests = new List<LeaveRequestListDto>();

        if (request.IsLoggedInUser)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(
                q => q.Type == CustomClaimTypes.Uid)?.Value;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await _userService.GetEmployee(userId);
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = employee;
            }
        }
        else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            foreach (var req in requests)
            {
                req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
            }
        }

        return requests;

            
    }
}