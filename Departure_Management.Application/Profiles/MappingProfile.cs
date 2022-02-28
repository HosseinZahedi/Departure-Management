using AutoMapper;
using Departure_Management.Application.DTOs.LeaveRequest;
using Departure_Management.Application.DTOs.LeaveType;
using Departure_Management.Domain;

namespace Departure_Management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestListDto>()
            .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(src => src.DateCreated))
            .ReverseMap();
        CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
        CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();

        CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
        CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
    }
}