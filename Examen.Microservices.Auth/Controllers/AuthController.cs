using Examen.Microservices.Auth.Models;
using Examen.Microservices.Auth.Models.Dto;
using Examen.Microservices.Auth.Services;
using Examen.Microservices.Auth.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace Examen.Microservices.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
          
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage.ToString()))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage.ToString();
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Usuario o passwors es incorrecto";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        
    }
}