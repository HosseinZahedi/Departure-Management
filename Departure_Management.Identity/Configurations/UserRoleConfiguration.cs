using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Departure_Management.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "74AD18D6-FCC0-4046-B6A7-D0DD3A246822",
                UserId = "35F898CE-3AC2-4F03-B000-1EA6C63A2139"
            },
            new IdentityUserRole<string>
            {
                RoleId = "BD82D480-420F-4BE4-A1D3-60FEC8842FDC",
                UserId = "FF14F892-C36F-437A-AA04-A5BFF6AA61ED"
            }

        );
    }
}