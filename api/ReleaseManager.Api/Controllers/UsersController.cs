#nullable disable

using Microsoft.AspNetCore.Mvc;
using ReleaseManager.Model;
using ReleaseManager.Api.Repositories;
using ReleaseManager.Api.ViewModels;
using ReleaseManager.Model.Enums;
using FirebaseAdmin.Auth;

namespace ReleaseManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepostitory;

        public UsersController(IUserRepository userRepostitory)
        {
            _userRepostitory = userRepostitory;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var users = await _userRepostitory.GetAllAsync();

            var userVms = users.Select(UserViewModel.FromModel);

            return Ok(userVms);
        }

    }
}
