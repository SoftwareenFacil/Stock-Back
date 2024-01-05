using Stock_Back.BLL.Models.DTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Back.BLL.Controllers.UserControllers
{
    public class AddUsersController
    {
        private AppDbContext _context;
        public AddUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<(bool, bool)> AddUser(UserInsertDTO user)
        {
            var userCreator = new UserPost(_context);
            var isUser = false;
            var userExist = false;
            User? userCreate = new User();
            if (!string.IsNullOrWhiteSpace(user.Name) &&
                !string.IsNullOrWhiteSpace(user.Email) &&
                !string.IsNullOrWhiteSpace(user.Password) &&
                !string.IsNullOrWhiteSpace(user.Phone))
            {
                var idGetter = new UserGetIdByEmail(_context);
                if(await idGetter.GetUserIdByEmail(user.Email) > 0)
                {
                    userExist = true;
                    return (isUser, userExist);
                }
                userCreate.Name = user.Name;
                userCreate.Email = user.Email;
                userCreate.Password = user.Password;
                userCreate.Phone = user.Phone;

                // Generar la hora actual en UTC
                DateTime utcNow = DateTime.UtcNow; // Es más directo y recomendable usar DateTime.UtcNow
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time"); // Para sistemas Windows
                DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, chileTimeZone);

                userCreate.Created = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
                userCreate.Updated = DateTime.SpecifyKind(chileTime, DateTimeKind.Utc);
                isUser = await userCreator.InsertUser(userCreate);
                return (isUser, userExist);
            }
            
            return (isUser, userExist);
        }
    }
}
