using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        Task<bool> SaveAsync();
    }
}
