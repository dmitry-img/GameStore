using System;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;
using GameStore.BLL.DTOs.Auth;
using GameStore.BLL.Interfaces;

namespace GameStore.Api.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;
        private readonly IValidationService _validationService;
        private readonly IValidator<RegistrationDTO> _registrationValidator;

        public AuthController(
            IAuthService authService,
            IValidationService validationService,
            IValidator<RegistrationDTO> registrationValidator)
        {
            _authService = authService;
            _validationService = validationService;
            _registrationValidator = registrationValidator;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegistrationDTO registrationDTO)
        {
            _validationService.Validate(registrationDTO, _registrationValidator);

            await _authService.RegisterAsync(registrationDTO);

            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginDTO loginDTO)
        {
            var response = await _authService.LoginAsync(loginDTO);

            return Json(response);
        }

        [Route("refresh")]
        [HttpPost]
        public async Task<IHttpActionResult> Refresh(string refreshToken)
        {
            if (refreshToken == null)
            {
                return BadRequest("Missing refreshToken");
            }

            var response = await _authService.RefreshAsync(refreshToken);

            return Json(response);
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IHttpActionResult> Logout(string userObjectId)
        {
            if (userObjectId == null)
            {
                return BadRequest("Missing userObjectId");
            }

            await _authService.LogoutAsync(userObjectId);

            return Ok();
        }
    }
}
