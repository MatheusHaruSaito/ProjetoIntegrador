using unolink.api.Application.Models.Dtos;
using unolink.domain.Enums;
using unolink.domain.ValueObjects;

namespace unolink.api.Application.Services.SearchService
{
    public interface ISearchService
    {
        Task<SearchResultDTO> SearchAsync(SearchType type, string q, int page, int pageSize);
    }
}
