using Microsoft.AspNetCore.Identity;

namespace Appbay.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
