using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoIntegradorAPI.DTOs.OngTicketDto;
using ProjetoIntegradorAPI.Models;
using ProjetoIntegradorAPI.Repositories.OngTicketRepository;

namespace ProjetoIntegradorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OngTicketController : ControllerBase
    {
        private readonly IOngTicketRepository _ongTicketRepository;
        public OngTicketController(IOngTicketRepository ongTicketRepository)
        {
            _ongTicketRepository = ongTicketRepository;
        }

        [HttpPost]
        public async Task<ActionResult<OngTicket>> PostAsync(PostOngTicketDto postOngTicket)
        {
            OngTicket ongTicket = await _ongTicketRepository.AddAsync(postOngTicket.PostOngTicketDtoToOngTicket());
            if (ongTicket is null)
            {
                return BadRequest("Failed To CreateTicket");
            }
            return Ok(ongTicket);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OngTicket>>> GetAll()
        {
            return Ok(await _ongTicketRepository.GetAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<OngTicket>> Get(Guid Id)
        {
            OngTicket ongTicket = await _ongTicketRepository.GetByIdAsync(Id);
            if (ongTicket is null)
            {
                return BadRequest("Failed To Get OngTicket");
            }
            return Ok(ongTicket);
        }

        [HttpPut("AcceptTicket/{Id}")]
        public async Task<ActionResult<OngTicket>> AcceptTicket(Guid Id)
        {
            OngTicket ongTicket = await _ongTicketRepository.AcceptTicket(Id);
            if (ongTicket is null)
            {
                return BadRequest("Failed To Get OngTicket");
            }
            return Ok(ongTicket);
        }

        [HttpPut("Decline/{Id}")]
        public async Task<ActionResult<OngTicket>> DeclineTicket(Guid Id)
        {
            OngTicket ongTicket = await _ongTicketRepository.DeclineTicket(Id);
            if (ongTicket is null)
            {
                return BadRequest("Failed To Get OngTicket");
            }
            return Ok(ongTicket);
        }
    }
}
