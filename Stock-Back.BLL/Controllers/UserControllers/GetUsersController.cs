using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.UserDTO;
using Stock_Back.DAL.Controllers.UserControllers;

namespace Stock_Back.BLL.Controllers.UserControllers
{
    public class GetUsersController
    {
        private AppDbContext _context;
        public GetUsersController(AppDbContext _dbContext)
        {
            _context = _dbContext;
        }

        public async Task<List<UserDTO>> GetUsersBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new UserGetBy(_context);
            var users = await userGetter.GetUserBy(id, name, email, created, vigency);
            if (users.Count() > 0)
            {
                TimeZoneInfo chileTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific SA Standard Time");
                List<UserDTO> result = new List<UserDTO>();
                foreach (var row in users)
                {
                    DateTime chileTime = TimeZoneInfo.ConvertTimeFromUtc(row.Created, chileTimeZone);
                    result.Add(new UserDTO()
                    {
                        Id = row.Id,
                        Name = row.Name,
                        Email = row.Email,
                        Phone = row.Phone,
                        Address = row.Address,
                        SuperAdmin = row.SuperAdmin,
                        Created = chileTime,
                        Vigency = row.Vigency
                    });
                }
                
                return result;
            }
            return new List<UserDTO>();
        }
    }
}
