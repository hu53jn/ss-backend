using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ss_backend.Interfaces;
using ss_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Data.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee); ;
        }

        public void DeleteEmployee(long employeeId)
        {
            var employee = _context.Employees.Find(employeeId);
            _context.Employees.Remove(employee); ;
        }

        public bool EmployeeExists(long employeeId)
        {
            return _context.Employees.Any(e => e.Id == employeeId);
        }

        public async Task<Employee> FindEmployee(long employeeId)
        {
            return await _context.Employees.FindAsync(employeeId); ;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync(); ;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            return await _context.Employees.FindAsync(id);

        }

        public bool UniqueUsername(string username)
        {
            var employee = _context.Employees.Where(x => x.Username == username);
            if(employee.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Employee> GetSecretSanta(string username)
        {
            return await _context.Employees.FirstAsync(x => x.Username == username);
        }

    }
}
