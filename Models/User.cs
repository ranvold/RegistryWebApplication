using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RegistryWebApplication.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
