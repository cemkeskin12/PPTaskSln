using Microsoft.AspNetCore.Identity;

namespace PPTask.Entity.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
