using Microsoft.AspNetCore.Mvc;
using ss_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        void AddEmployee(Employee employee);
        void DeleteEmployee(long employeeId);
        Task<Employee> FindEmployee(long employeeId);
        bool EmployeeExists(long employeeId);
        Task<bool> SaveAsync();
        Task<ActionResult<Employee>> GetEmployee(long id);
        bool UniqueEmail(string email);
        Task<Employee> GetSecretSanta(string email);

    }
}
