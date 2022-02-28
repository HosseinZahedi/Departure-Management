using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Departure_Management.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "BD82D480-420F-4BE4-A1D3-60FEC8842FDC",
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
            new IdentityRole
            {
                Id = "74AD18D6-FCC0-4046-B6A7-D0DD3A246822",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}