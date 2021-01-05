using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is mandatory field!")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is mandatory field!")]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
