using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        //Post : baseUrl/api/Authentication/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var Result = await _authenticationService.LoginAsync(loginDTO);
            return HandleResult(Result);
        }

        //Post : baseUrl/api/Authentication/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var Result = await _authenticationService.ResgisterAsync(registerDTO);
            return HandleResult(Result);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await _authenticationService.CheckEmailAsync(Email);
            return Ok(Result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _authenticationService.GetUserByEmail(Email);
            return HandleResult(user);
        }

    }
}
