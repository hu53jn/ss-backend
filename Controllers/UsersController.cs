using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ss_backend.Data;
using ss_backend.Dtos;
using ss_backend.Helpers;
using ss_backend.Interfaces;
using ss_backend.Models;

namespace ss_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //POST api/user/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var _loginResDto =  _unitOfWork.UserRepository.Authenticate(loginDto.Email, loginDto.Password);
            if(_loginResDto == null)
            {
                return Unauthorized();
            }

            var loginResDto = new LoginResDto();
            loginResDto.Email = _loginResDto.Email;
            loginResDto.Token = _loginResDto.Token;
            loginResDto.Role = _loginResDto.Role;

            return Ok(loginResDto);

        }

        //POST api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            bool uniqueEmail = _unitOfWork.UserRepository.UniqueEmail(user.Email);

            if (uniqueEmail)
            {
                return BadRequest();
            }
            _unitOfWork.UserRepository.RegisterUser(user);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        //GET api/user/getUserInfo
        [HttpGet("getUserInfo/{email}")]
        public ActionResult<UserInfoDto> GetSecretSanta(string email)
        {
            User user =  _unitOfWork.UserRepository.GetUserInfo(email);

            if (user != null)
            {
                UserInfoDto userInfoDto = new UserInfoDto();
                userInfoDto.FirstName = user.FirstName;
                userInfoDto.LastName = user.LastName;
                return userInfoDto;
            }
            else
            {
                return NotFound();
            }
        }

    }
}
