using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Examen.Web.Models;
using Examen.Web.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Examen.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public LoginController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]  LoginRequestDto obj)
        {
            
            try
            {
                ResponseDto? responseDto = await _authService.LoginAsync(obj);

                if (responseDto != null && responseDto.IsSuccess)
                {
                    LoginResponseDto loginResponseDto =
                        JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                    await SignInUser(loginResponseDto);
                    _tokenProvider.SetToken(loginResponseDto.Token);
                    // return RedirectToAction("Index", "Home");
                    return Ok(responseDto);
                }
                else
                {
                    TempData["error"] = responseDto.Message;
              
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(obj);
        }
        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));





            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
