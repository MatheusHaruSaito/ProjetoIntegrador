using unolink.domain.Models;

namespace unolink.api.Application.Models.Request
{
    public class CreateVoteRequest
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}
