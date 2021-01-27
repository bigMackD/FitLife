using FitLife.DB.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitLife.DB.Context
{
    public class AuthenticationContext : IdentityDbContext<AppUser>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options):base(options)
        {
                
        }

    }

}
