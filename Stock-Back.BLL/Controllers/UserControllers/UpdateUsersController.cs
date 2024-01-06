using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Models.DTO;
using Stock_Back.DAL.Controllers.UserControllers;

namespace Stock_Back.BLL.Controllers.UserControllers
{
    public class UpdateUsersController
    {
        private AppDbContext _context;
        public UpdateUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<ResponseType> UpdateUser(UserEditDTO userEdited)
        {
            var userVerify = new UserGetById(_context);
            var userUpdater = new UserUpdate(_context);
            var user = await userVerify.GetUserById(userEdited.Id);
            if(user == null)
            {
                return ResponseType.NotFound;
            }

            if (string.IsNullOrWhiteSpace(userEdited.Name) && string.IsNullOrWhiteSpace(userEdited.Email) && string.IsNullOrWhiteSpace(userEdited.Password) && string.IsNullOrWhiteSpace(userEdited.Phone))
            {
                return ResponseType.Failure;
            }

            if (!string.IsNullOrWhiteSpace(userEdited.Name))
            {
                user.Name = userEdited.Name;
            }

            if (!string.IsNullOrWhiteSpace(userEdited.Email))
            {
                user.Email = userEdited.Email;
            }

            if (!string.IsNullOrWhiteSpace(userEdited.Phone))
            {
                user.Phone = userEdited.Phone;
            }

            if (!string.IsNullOrWhiteSpace(userEdited.Password))
            {
                user.Password = userEdited.Password;
            }

            var userUpdated = await userUpdater.UpdateUser(user);
            if(userUpdated == null)
            {
                return ResponseType.Failure;
            }
            
            return ResponseType.Success;
        }
    }
}
