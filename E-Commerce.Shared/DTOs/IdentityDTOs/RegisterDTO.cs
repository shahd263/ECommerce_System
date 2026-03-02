using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.IdentityDTOs
{
    public record RegisterDTO(string Email , string Password , string DisplayName , string UserName , [Phone]string PhoneNumber);
    
}
