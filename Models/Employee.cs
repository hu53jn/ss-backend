using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Models
{
    public class Employee
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Username is mandatory field!")]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string SecretSanta { get; set; }
        public long CompanyId { get; set; }

        public static implicit operator Employee(ActionResult<Employee> v)
        {
            throw new NotImplementedException();
        }
    }
}
