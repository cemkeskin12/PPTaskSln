using Microsoft.AspNetCore.Identity;

namespace PPTask.Entity.Models
{
    public class AppUserTokens : IdentityUserToken<string>
    {
        public DateTime ExpireDate { get; set; }
    }
}
