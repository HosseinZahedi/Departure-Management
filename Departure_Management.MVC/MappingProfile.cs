using AutoMapper;
using Departure_Management.MVC.Models;
using Departure_Management.MVC.Services.Base;

namespace Departure_Management.MVC;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVM>().ReverseMap();
        CreateMap<LeaveRequestDto, LeaveRequestVM>()
            .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveRequestListDto, LeaveRequestVM>()
            .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();
        CreateMap<ChangePasswordVM, ChangePasswordRequest>().ReverseMap();
        CreateMap<EmployeeVM, Employee>().ReverseMap();
    }
}