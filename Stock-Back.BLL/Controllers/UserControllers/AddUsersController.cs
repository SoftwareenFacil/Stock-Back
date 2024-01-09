using Stock_Back.BLL.Models.UserDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.DAL.Models;

namespace Stock_Back.BLL.Controllers.UserControllers
{
    public class AddUsersController
    {
        private AppDbContext _context;
        public AddUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<int> AddUser(UserInsertDTO user)
        {

            var userSample = new UserGetByEmail(_context);
            if (await userSample.GetUserByEmail(user.Email) != null)
                return -1;

            var userCreator = new UserPost(_context);
            var userCreate = new User();
            var hasher = new Hasher();

            userCreate.Name = user.Name;
            userCreate.Email = user.Email;
            userCreate.Password = hasher.HashPassword(user.Password);
            userCreate.Phone = user.Phone;

            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
            DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);

            userCreate.Created = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
            userCreate.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
            return await userCreator.InsertUser(userCreate);


        }
    }
}
