using ss_backend.Dtos;
using ss_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ss_backend.Interfaces
{
    public interface IUserRepository
    {
        LoginResDto Authenticate(string username, string password);
        void RegisterUser(User user);
        User GetUserInfo(string username);
        Task<bool> SaveAsync();
        bool UniqueUsername(string username);

    }
}
