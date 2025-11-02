using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Models.ViewModels;
using unolink.api.Application.Models.ViewModels.ViewModelExtension;
using unolink.api.Application.Services.ImagesSevice;
using unolink.api.Application.Services.UserService;

namespace unolink.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFilesService _fileService;
        public UserController(IUserService userService,IFilesService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var result = await _userService.Add(request);
            if (!result)
                return BadRequest();

            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<UserPostViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userService.GetAll();

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
            var data = await _userService.GetById(id);
            if (data is null)
                return NotFound();

            return Ok(data);
        }
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";
            
            var result = await _userService.Update(request, baseUrl);
            if (!result) { 
                await _fileService.DeleteFile(request.ProfileImgPath);
                return BadRequest();
            }
                return Ok();
        }
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UserDeactivate(Guid id)
        {
            var result = await _userService.UserTriggerActive(id);

            if (!result) return NotFound();

            return Ok(result);
        }
        [HttpGet("Profile/{Id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProfileInfo(Guid Id)
        {
            var data = await _userService.GetById(Id);

            if (data is null) return NotFound();

            return Ok(data.ToProfileViewModel());
        }
        [HttpGet("Email/{email}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var data = await _userService.GetByEmail(email);

            if (data is null) return NotFound();

            return Ok(data.ToViewModel());
        }

    }
}