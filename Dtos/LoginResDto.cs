using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Dtos
{
    public class LoginResDto
    {
        [Required]
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
