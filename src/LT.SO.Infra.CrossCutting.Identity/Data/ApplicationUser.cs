using Microsoft.AspNetCore.Identity;

namespace LT.SO.Infra.CrossCutting.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool Active { get; set; }

        public bool FirstPass { get; set; }
    }
}