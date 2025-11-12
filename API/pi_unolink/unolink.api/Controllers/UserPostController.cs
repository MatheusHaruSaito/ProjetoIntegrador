using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using unolink.api.Application.Models.Request;
using unolink.api.Application.Models.ViewModels;
using unolink.api.Application.Models.ViewModels.ViewModelExtension;
using unolink.api.Application.Services.ImagesSevice;
using unolink.api.Application.Services.UserPostService;

namespace unolink.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly IUserPostService _userPostService;
        private readonly IFilesService _fileService;

        public UserPostController(IUserPostService userPostService, IFilesService filesService)
        {
            _userPostService = userPostService;
            _fileService = filesService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreatePostRequest request)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            var result = await _userPostService.Add(request, baseUrl);
            if (!result)
                return BadRequest();

            return Ok();
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<UserViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userPostService.GetAll();

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
            var data = await _userPostService.GetById(id);
            if (data is null)
                return NotFound();

            return Ok(data);
        }
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateUserPostRequest request)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}";

            var result = await _userPostService.Update(request, baseUrl);
            if (!result)
            {
                await _fileService.DeleteFile(request.PostImgPath);
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("Comment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Coment(CreateCommentRequest request)
        {
            var result = await _userPostService.Comment(request);
            if (!result)
            {
                return BadRequest("Failed To comment");
            }
            return Ok("Commented");
        }
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UserDeactivate(Guid id)
        {
            var result = await _userPostService.UseTriggerActive(id);

            if (!result) return NotFound();

            return Ok(result);
        }
        [HttpPost("Vote")]

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> VotePost(CreateVoteRequest request)
        {
            var data = await _userPostService.Vote(request);

            if (!data) return NotFound();

            return Ok();
        }

        [HttpPost("CommentVote")]

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CommentVote(CreateCommentVoteRequest request)
        {
            var data = await _userPostService.VoteComment(request);

            if (!data) return NotFound();

            return Ok();
        }
    }
}
