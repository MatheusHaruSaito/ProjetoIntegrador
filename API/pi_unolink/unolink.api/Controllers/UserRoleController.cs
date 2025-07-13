using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using unolink.api.Application.Models.ViewModels.ViewModelExtension;
using unolink.api.Application.Services.UserRoleService;

namespace unolink.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var data = _userRoleService.GetAll();

            var result = data.Select(x => x.ToViewModel());

            return Ok(result);
        }

    }
}