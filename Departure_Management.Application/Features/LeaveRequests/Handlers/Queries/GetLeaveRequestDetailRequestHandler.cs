using AutoMapper;
using Departure_Management.Application.Contracts.Identity;
using Departure_Management.Application.Contracts.Persistence;
using Departure_Management.Application.DTOs.LeaveRequest;
using Departure_Management.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace Departure_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IUserService userService)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveRequest = _mapper.Map<LeaveRequestDto>(await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
        leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);
        return leaveRequest;
    }
}