using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_backend.Data;
using ss_backend.Data.Repo;
using ss_backend.Dtos;
using ss_backend.Interfaces;
using ss_backend.Models;

namespace ss_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _unitOfWork.EmployeeRepository.GetEmployeesAsync();
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Update not allowed!");
            }

            var employeeFromDb = await _unitOfWork.EmployeeRepository.FindEmployee(id);
            if( employeeFromDb == null)
            {
                return BadRequest("Employee does not exist!");
            }

            employeeFromDb.FirstName = employee.FirstName;
            employeeFromDb.LastName = employee.LastName;
            employeeFromDb.SecretSanta = employee.SecretSanta;
            await _unitOfWork.SaveAsync();
            return StatusCode(200);

        }

        // POST: api/Employee
        [HttpPost("post")]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee();
            employee.Email = employeeDto.Email;
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.SecretSanta = employeeDto.SecretSanta;
            employee.CompanyId = 1;

            bool uniqueEmail = _unitOfWork.EmployeeRepository.UniqueEmail(employeeDto.Email);

            if (uniqueEmail)
            {
                return BadRequest("Employee already added!");
            }
            else
            {
                _unitOfWork.EmployeeRepository.AddEmployee(employee);
                await _unitOfWork.SaveAsync();

                return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
            }

           
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(long id)
        {
            var employee = await _unitOfWork.EmployeeRepository.FindEmployee(id);
            if(employee == null)
            {
                return NotFound();
            }

            _unitOfWork.EmployeeRepository.DeleteEmployee(id);
            await _unitOfWork.SaveAsync();

            return employee;

        }

        //GET api/employee/getSecretSanta
        [HttpGet("getSecretSanta/{email}")]
        public async Task<ActionResult<EmployeeDto>> GetSecretSanta(string email)
        {
            Employee secretSanta = await _unitOfWork.EmployeeRepository.GetSecretSanta(email);
            
            if(secretSanta != null)
            {
                EmployeeDto employeeDto = new EmployeeDto();
                employeeDto.Email = secretSanta.Email;
                employeeDto.FirstName = secretSanta.FirstName;
                employeeDto.LastName = secretSanta.LastName;
                employeeDto.SecretSanta = secretSanta.SecretSanta;
                return employeeDto;
            }
            else
            {
                return NotFound();
            }
        }



    }
}
