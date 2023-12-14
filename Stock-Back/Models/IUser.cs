
using Stock_Back.DAL;

namespace Stock_Back.Models
{
    public interface IUser
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task <User> GetUserById(int id);
        public Task<bool> InsertUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);

    }
}
