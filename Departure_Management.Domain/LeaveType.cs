using Departure_Management.Domain.Common;

namespace Departure_Management.Domain;

public class LeaveType : BaseDomainEntity
{
    public string Name { get; set; }
    public int DefaultDays { get; set; }
}