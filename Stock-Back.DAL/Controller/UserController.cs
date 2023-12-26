using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Controller.UserControllers;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Interfaces;
using Stock_Back.DAL.Models;


namespace Stock_Back.DAL.Controller
{
    public class UserController : IUserController
    {
        private AppDbContext _context;
        private UserGetAll userGetAll;
        private UserGetById userGetById;
        private UserPost userPost;
        private UserUpdate userUpdate;
        private UserDelete userDelete;
        private UserGetIdByEmail userGetIdByEmail;
        private UserExistsById userExistById;
        public UserController(AppDbContext context)
        {
            _context = context;
            userGetAll = new UserGetAll(_context);
            userGetById = new UserGetById(_context);
            userPost = new UserPost(_context);
            userUpdate = new UserUpdate(_context);
            userDelete = new UserDelete(_context);
            userGetIdByEmail = new UserGetIdByEmail(_context);
            userExistById = new UserExistsById(_context);
        }

        //GET
        public async Task<List<User>> GetAllUsers()
        {
            return await userGetAll.GetAllUsers();
        }

        //GET BY ID
        public async Task<User?> GetUserById(int id)
        {
            return await userGetById.GetUserById(id);
        }

        //POST
        public async Task<User?> InsertUser(User user)
        {
            return await userPost.InsertUser(user);
        }

        //PUT
        public async Task<User?> UpdateUser(User user)
        {
            return await userUpdate.UpdateUser(user);
        }

       //GET ID BY EMAIL
        public async Task<int> GetUserIdByEmail(string email)
        {
            return await userGetIdByEmail.GetUserIdByEmail(email);
        }

        //DELETE
        public async Task<bool> DeleteUser(int id)
        {
            return await userDelete.DeleteUser(id);
        }

        //GET TRUE OR FALSE IF USER EXISTS
        public async Task<bool> UserEmailExists(string email)
        {
            return await userExistById.UserEmailExists(email);
        }
    }
}
