using Stock_Back.DAL.Models;

namespace Stock_Back.DAL.Interfaces
{
    public interface IUserController
    {
        public Task<List<User>> GetAllUsers();
        public Task<User?> GetUserById(int id);
        public Task<int> GetUserIdByEmail(string email);
        public Task<User?> InsertUser(User user);
        public Task<User?> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);
        public Task<bool> UserEmailExists(string email);
    }
}
