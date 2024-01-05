using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.DTO;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.DAL.Models;

namespace Stock_Back.BLL.Controllers.UserControllers
{
    public class GetUsersController
    {
        private AppDbContext _context;
        public GetUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<dynamic> GetUsers(int id)
        {
            if(id == 0)
                return await GetAllUsers();
            return await GetUserById(id);
        }
        public async Task<UserDTO> GetUserById(int id)
        {
            var userGetter = new UserGetById(_context);
            var user = await userGetter.GetUserById(id);
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                SuperAdmin = user.SuperAdmin
            };
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var userGetter = new UserGetAll(_context);
            var users = await userGetter.GetAllUsers();
            List<UserDTO> result = new List<UserDTO>();

            users.ForEach(row => result.Add(new UserDTO()
            {
                Id = row.Id,
                Name = row.Name,
                Email = row.Email,
                Phone = row.Phone,
                SuperAdmin = row.SuperAdmin
            }));
            return result;
        }

    }
}
