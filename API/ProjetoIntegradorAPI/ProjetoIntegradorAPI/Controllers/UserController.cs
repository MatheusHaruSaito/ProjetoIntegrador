using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.DTOs.UserDto;
using ProjetoIntegradorAPI.Models;
using ProjetoIntegradorAPI.Repositories;

namespace ProjetoIntegradorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(PostUserDto postUserDto)
        {
            var user = postUserDto.PostUserDtoToUser();
            if(user == null)
            {
                return NotFound();
            }
            await _userRepository.AddAsync(user);
            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var users = await _userRepository.GetAllAsync();
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUserById(Guid Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{Id}")]

        public async Task<ActionResult<User>> TriggerUserActive(Guid Id)
        {
            bool IsUserActive = await _userRepository.UserTriggerActive(Id);
            return Ok(IsUserActive);
        }
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(PutUserDto UpdateUser)
        {
            var user = await _userRepository.Update(UpdateUser.PutUserDtoToUser());
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
