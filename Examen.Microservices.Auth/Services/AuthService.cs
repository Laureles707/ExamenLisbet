using Examen.Microservices.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using Examen.Microservices.Auth.Services.IService;
using Examen.Microservices.Auth.Models.Dto;
using Examen.Microservices.Auth.Data;

namespace Examen.Microservices.Auth.Services
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public AuthService(ApplicationDBContext db, UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {

            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }



        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u=>u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if(user== null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

     
            var token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;

        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return ""  ;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
            }
            return "Error encontrado";
        }

      


    }
}
