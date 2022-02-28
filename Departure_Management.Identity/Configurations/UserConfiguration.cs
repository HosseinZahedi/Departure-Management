using Departure_Management.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Departure_Management.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "35F898CE-3AC2-4F03-B000-1EA6C63A2139",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "Admin",
                LastName = "Localhost",
                UserName = "admin@localhost.com",
                PhoneNumber = "08616093462",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "Abc123*"),
                EmailConfirmed = true
            },
            new ApplicationUser
            {
                Id = "FF14F892-C36F-437A-AA04-A5BFF6AA61ED",
                Email = "employee@localhost.com",
                NormalizedEmail = "EMPLOYEE@LOCALHOST.COM",
                FirstName = "Employee",
                LastName = "Localhost",
                UserName = "employee@localhost.com",
                PhoneNumber = "09264821765",
                PhoneNumberConfirmed = true,
                NormalizedUserName = "EMPLOYEE@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "Abc123*"),
                EmailConfirmed = true
            }
        );
    }
}