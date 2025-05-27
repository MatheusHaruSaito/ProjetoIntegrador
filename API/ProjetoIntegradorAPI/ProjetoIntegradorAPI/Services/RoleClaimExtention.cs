using ProjetoIntegradorAPI.Models;
using System.Security.Claims;

namespace ProjetoIntegradorAPI.Services
{
    public static class RoleClaimExtention
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>()
            {
                new(ClaimTypes.Name,user.Name),
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Role,user.Role.ToString())
            };
            return result;
        }
    }
}
