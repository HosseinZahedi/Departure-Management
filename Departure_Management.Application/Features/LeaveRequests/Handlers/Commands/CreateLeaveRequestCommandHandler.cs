using AutoMapper;
using Departure_Management.Application.Constants;
using Departure_Management.Application.Contracts.Infrastructure;
using Departure_Management.Application.Contracts.Persistence;
using Departure_Management.Application.DTOs.LeaveRequest.Validators;
using Departure_Management.Application.Features.LeaveRequests.Requests.Commands;
using Departure_Management.Application.Responses;
using Departure_Management.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Departure_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        IUnitOfWork unitOfWork,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
            q => q.Type == CustomClaimTypes.Uid)?.Value;

        //var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
        //if(allocation is null)
        //{
        //    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveRequestDto.LeaveTypeId),
        //        "You do not have any allocations for this leave type."));
        //}
        //else
        //{
        //    int daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
        //    if (daysRequested > allocation.NumberOfDays)
        //    {
        //        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
        //            nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
        //    }
        //}
            
        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Request Failed";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        else
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaveRequest.RequestingEmployeeId = userId;
            leaveRequest.DateRequested = DateTime.Now;
            leaveRequest = await _unitOfWork.LeaveRequestRepository.Add(leaveRequest);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Request Created Successfully";
            response.Id = leaveRequest.Id;

            try
            {
                //var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

                //var email = new Email
                //{
                //    To = emailAddress,
                //    Body = $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                //           $"has been submitted successfully.",
                //    Subject = "Leave Request Submitted"
                //};

                //await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                //// Log or handle error, but don't throw...
            }
        }
            
        return response;
    }
}