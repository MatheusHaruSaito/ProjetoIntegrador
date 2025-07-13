using System.Security.Claims;

namespace unolink.api.Application.Models.Dtos.DtoExtension
{
    public static class ClaimsExtensions
    {
        public static IEnumerable<Claim> GetClaims(this UserClaimDTO user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                //new Claim(ClaimTypes.Role, user.Role)
            };
        }
    }
}
