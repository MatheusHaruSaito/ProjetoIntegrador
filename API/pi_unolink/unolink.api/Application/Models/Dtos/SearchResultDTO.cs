using unolink.domain.ValueObjects;
namespace unolink.api.Application.Models.Dtos
{
    public class SearchResultDTO
    {
        public IEnumerable<UserDTO> User { get; set; } = new List<UserDTO>();
        public IEnumerable<UserPostDTO> Posts { get; set; } = new List<UserPostDTO>();
        public PaginationData paginationData { get; set; }

    }
}
