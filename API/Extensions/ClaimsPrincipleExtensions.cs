using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userDate = await userManager.Users
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            if (userDate == null) throw new AuthenticationException("User not found");
            return userDate;
        }

        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userDate = await userManager.Users
                .Include(a => a.Address)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            if (userDate == null) throw new AuthenticationException("User not found");
            return userDate;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email)
                ?? throw new AuthenticationException("Email claim not found");
            return email;
        }
    }
}
