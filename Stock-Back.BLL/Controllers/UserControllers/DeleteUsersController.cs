using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;


namespace Stock_Back.BLL.Controllers.UserControllers
{
    public class DeleteUsersController
    {
        private AppDbContext _context;
        public DeleteUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<bool> DeleteUserById(int id)
        {
            var userVerify = new UserGetById(_context);
            var exist =  await userVerify.GetUserById(id);
            if (exist == null)
            {
                return false;
            }

            var userDeleter = new UserDelete(_context);
            return await userDeleter.DeleteUser(id);
        }
    }
}
