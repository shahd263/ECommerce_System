using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IAuthenticationService
    {
        // Login 
        // Email , Password => Token ,DisplayName , Email
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);

        // Register 
        //Email , Password , UserName , DisplayName , PhoneNumber => Token , DisplayName , Email
        Task<Result<UserDTO>> ResgisterAsync(RegisterDTO registerDTO);
        Task<bool> CheckEmailAsync(string Email);
        Task<Result<UserDTO>> GetUserByEmail(string Email);
    }
}
