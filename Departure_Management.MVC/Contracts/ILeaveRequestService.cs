using Departure_Management.MVC.Models;
using Departure_Management.MVC.Services.Base;

namespace Departure_Management.MVC.Contracts;

public interface ILeaveRequestService
{
    Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
    Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
    Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);
    Task<LeaveRequestVM> GetLeaveRequest(int id);
    Task DeleteLeaveRequest(int id);
    Task ApproveLeaveRequest(int id, bool? approved);
        
}