﻿using Departure_Management.Application.DTOs.LeaveRequest;
using MediatR;

namespace Departure_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestDetailRequest : IRequest<LeaveRequestDto>
{
    public int Id { get; set; }
}