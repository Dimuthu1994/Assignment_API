using Microsoft.AspNetCore.Identity;

namespace Assignment_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
