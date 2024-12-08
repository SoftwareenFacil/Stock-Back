using EIC_Back.DAL.Context;
using EIC_Back.BLL.Models.UserModelDTO;
using EIC_Back.DAL.Repository;
using AutoMapper;
using EIC_Back.DAL.Models;
using EIC_Back.DAL.Utilities;

namespace EIC_Back.BLL.Services
{
    public class UserService
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;
        public UserService(AppDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
            _userRepository = new UserRepository(dbContext);

        }
        public async Task<bool> DeleteUserById(int id)
        {
            var exist = await _userRepository.GetUserById(id);
            if (exist == null)
            {
                return false;
            }

            return await _userRepository.DeleteUser(id);
        }
        public async Task<int> AddUser(UserInsertDTO user)
        {
            if (await NewMailAlreadyExists(user.Email))
                return -1;

            var userCreate = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = Hasher.HashPassword(user.Password),
                Job = user.Job,
                Phone = user.Phone,
                SuperAdmin = user.SuperAdmin,
                Vigency = true,
                Created = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            };

            return await _userRepository.InsertUser(userCreate);
        }
        public async Task<dynamic?> GetUsers(int id)
        {
            if (id == 0)
                return await GetAllUsers();
            return await GetUserById(id);
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<UserDTO?>(user);
            }
        }

        public async Task<List<UserDTO>?> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();

            if (users.Count() > 0)
            {
                return _mapper.Map<List<UserDTO>?>(users);
            }
            else
            {
                return null;
            }

        }
        public async Task<(bool, bool)> UpdateUser(UserEditDTO userEdited)
        {
            bool isUpdated = false;
            bool isUser = false;
            if (string.IsNullOrWhiteSpace(userEdited.Name) && string.IsNullOrWhiteSpace(userEdited.Email) && string.IsNullOrWhiteSpace(userEdited.Password) && string.IsNullOrWhiteSpace(userEdited.Phone))
                return (isUpdated, isUser);

            var userUpdater = new UserRepository(_context);
            var user = await _userRepository.GetUserById(userEdited.Id);
            if (user != null)
            {
                isUser = true;

                if (await NewMailAlreadyExists(userEdited.Email) && userEdited.Email != user.Email)
                    return (isUpdated, isUser);
                
                user.Name = !string.IsNullOrEmpty(userEdited.Name) ? userEdited.Name : user.Name;
                user.Email = !string.IsNullOrEmpty(userEdited.Email) ? userEdited.Email : user.Email;
                user.Password = CheckifNewPassword(userEdited.Password, user.Password);
                user.Phone = !string.IsNullOrEmpty(userEdited.Phone) ? userEdited.Phone : user.Phone;               
                user.Job = !string.IsNullOrEmpty(userEdited.Job) ? userEdited.Job : user.Job;
                user.SuperAdmin = userEdited.SuperAdmin ?? user.SuperAdmin;
                user.Updated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
                user.Vigency = userEdited.Vigency ?? user.Vigency;
                isUpdated = await userUpdater.UpdateUser(user);
                return (isUpdated, isUser);
            }
            return (isUpdated, isUser);
        }

        private string CheckifNewPassword(string password, string userpassword)
        {
            if (!string.IsNullOrEmpty(password))
            {
                return Hasher.HashPassword(password);
            }
            return userpassword;

        }
        private async Task<bool> NewMailAlreadyExists(string mail)
        {
            if (!string.IsNullOrEmpty(mail))
                if (await _userRepository.GetUserByEmail(mail) != null)
                    return true;
            return false;
            
        }

    }
}
