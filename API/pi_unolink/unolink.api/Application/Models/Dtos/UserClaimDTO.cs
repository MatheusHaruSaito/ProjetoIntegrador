using unolink.domain.Models;

namespace unolink.api.Application.Models.Dtos
{
    public class UserClaimDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
