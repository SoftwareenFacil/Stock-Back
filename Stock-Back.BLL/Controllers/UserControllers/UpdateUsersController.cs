using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Models.DTO;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.DAL.Models;

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
            if (user == null)
                return ResponseType.NotFound;

            if (string.IsNullOrWhiteSpace(userEdited.Name) && string.IsNullOrWhiteSpace(userEdited.Email) && string.IsNullOrWhiteSpace(userEdited.Password) && userEdited.Phone == 0)
                return ResponseType.Failure;

            user.Name = !string.IsNullOrEmpty(userEdited.Name) ? userEdited.Name : user.Name;
            user.Email = !string.IsNullOrEmpty(userEdited.Email) ? userEdited.Email : user.Email;

            user.Password = CheckifNewPassword(userEdited.Password, user.Password);

            if (userEdited.Phone == 0)
                user.Phone = userEdited.Phone;

            var userUpdated = await userUpdater.UpdateUser(user);
            if (userUpdated == null)
                return ResponseType.Failure;
            return ResponseType.Success;
        }

        private string CheckifNewPassword(string password, string userpassword)
        {
            if (!string.IsNullOrEmpty(password))
            {
                var hasher = new Hasher();
                return hasher.HashPassword(password);
            }
            return userpassword;

        }
    }
}
