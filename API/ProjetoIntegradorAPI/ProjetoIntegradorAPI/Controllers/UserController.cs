using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.DTOs.User;
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

        [HttpDelete("{Id}")]

        public async Task<ActionResult<User>> DeleteUser(Guid Id)
        {
            var user = await _userRepository.Delete(Id);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
        //Create a Dto for this later
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser(User UpdateUser)
        {
            UpdateUser.updateDate = DateTime.Now;
            var user = await _userRepository.Update(UpdateUser);
            if(user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }
    }
}
