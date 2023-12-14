using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Back.DAL.Controller.Interfaces
{
    public interface IUserController
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task<bool> InsertUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);
    }
}
