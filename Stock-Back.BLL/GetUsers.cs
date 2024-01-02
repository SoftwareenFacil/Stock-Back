using Stock_Back.BLL.Models;
using Stock_Back.DAL.Controller.UserControllers;
using Stock_Back.DAL.Data;

namespace Stock_Back.BLL
{
    public class GetUsers 
    {
        private readonly UserGetAll _users;
        public GetUsers(AppDbContext context)
        {
            _users = new UserGetAll(context);
        }
        
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var response = new List<UserDTO>();
            try
            {
                var users = await _users.GetAllUsers();
                
                if (!users.Any())
                {
                    return response;
                }
                foreach(var user in users)
                {
                    response.Add(new UserDTO()
                    {
                        Name = user.Name,
                        Id = user.Id,
                        Email = user.Email,
                        Created = user.Created,
                        Updated = user.Updated,
                        SuperAdmin = user.SuperAdmin
                    });
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
