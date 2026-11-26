using Microsoft.AspNetCore.Identity;

namespace SoK.Webb.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public Guid? CustomerId { get; set; }
    public Guid? EmployeeId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public bool IsActive { get; set; } = true;
}

