using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Interfaces
{
    public interface IUserController
    {
        public Task<List<User>> GetAllUsers();
        public Task<User?> GetUserById(int id);
        public Task<bool> InsertUser(User user);
        public Task<User?> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);
    }
}
