using Microsoft.Extensions.Options;
using ss_backend.Data.Repo;
using ss_backend.Helpers;
using ss_backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;
        public UnitOfWork(DataContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }


        public IUserRepository UserRepository => new UserRepository(_context, _appSettings);

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0; ;
        }
    }
}
