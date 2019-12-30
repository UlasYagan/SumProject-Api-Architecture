using System;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sum.Model.Auth;
using Sum.Service.Interface;

namespace Sum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ProductController));
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(nameof(Register))]
        public ActionResult<AuthenticationResult> Register([FromBody] UserRegisterDto request)
        {
            try
            {
                return Ok(_userService.Register(request));
            }
            catch (Exception ex)
            {
                _log.Error($"UserController Register method - {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}