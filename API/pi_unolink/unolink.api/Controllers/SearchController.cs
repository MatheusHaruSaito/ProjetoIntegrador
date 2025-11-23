using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using unolink.api.Application.Models.Dtos;
using unolink.api.Application.Services.SearchService;
using unolink.domain.Enums;
using unolink.domain.ValueObjects;

namespace unolink.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string q, [FromQuery] SearchType type = SearchType.All, [FromQuery] int page = 1,[FromQuery] int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest("Query Não pode ser nula");
            }
            
            var data = await _searchService.SearchAsync(type,q,page,pageSize);
            if (data is null)
            {
                return NotFound("Nenhum Resultado encontrado");
            }

            return Ok(data);
        }
    }
}
