using System.Data;
using System.Security.Claims;

namespace unolink.api.Application.Models.Dtos.DtoExtension
{
    public static class ClaimsExtensions
    {
        public static IEnumerable<Claim> GetClaims(this UserClaimDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var role in user.Role)
            {
               claims.Add( new Claim(ClaimTypes.Role, role));
            }

            return claims;
           
        }
    }
}

