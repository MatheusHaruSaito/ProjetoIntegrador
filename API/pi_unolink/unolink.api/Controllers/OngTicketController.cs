using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using unolink.api.Application.Models.Request.OngTicket;
using unolink.api.Application.Models.ViewModels;
using unolink.api.Application.Models.ViewModels.ViewModelExtension;
using unolink.api.Application.Services.OngTicketService;

namespace unolink.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OngTicketController : ControllerBase
    {
        private readonly IOngTicketService _ongTicketService;
        public OngTicketController(IOngTicketService ongTicketService)
        {
            _ongTicketService = ongTicketService;
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateOngTicketRequest request)
        {
            var result = await _ongTicketService.Add(request);

            if (!result)
                return BadRequest();

            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<OngTicketViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _ongTicketService.GetAll();

            if (!data.Any())
                return NoContent();

            var result = data.Select(x => x.ToViewModel());

            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _ongTicketService.GetById(id);
            if (data is null)
                return NotFound();

            return Ok(data);
        }
        [HttpPut("{id}/accept")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AcceptTicket(Guid id)
        {
            var result = await _ongTicketService.AcceptTicket(id);
            if (!result)
                return BadRequest();

            return Ok(result);
        }
        [HttpPut("{id}/decline")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeclineTicket(Guid id)
        {
            var result = await _ongTicketService.DeclineTicket(id);
            if (!result)
                return BadRequest();

            return Ok(result);
        }

    } 
}