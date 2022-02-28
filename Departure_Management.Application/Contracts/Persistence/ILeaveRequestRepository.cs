using Departure_Management.Domain;

namespace Departure_Management.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
    Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
}