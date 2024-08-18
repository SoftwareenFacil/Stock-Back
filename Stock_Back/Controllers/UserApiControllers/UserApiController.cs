using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.UserModelDTO;
using AutoMapper;

namespace Stock_Back.Controllers.UserApiControllers
{
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserApiController : ControllerBase
    {
        private AppDbContext _context;
        private readonly IMapper _mapper;

        public UserApiController(AppDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int id)
        {
            var userGetter = new UsersResponseController(_context, _mapper);
            return await userGetter.GetResponseUsers(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertDTO user)
        {
            var add = new UsersResponseController(_context, _mapper);
            return await add.Insert(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserEditDTO userEdited)
        {
            var updater = new UsersResponseController(_context, _mapper);
            return await updater.Update(userEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleter = new UsersResponseController(_context, _mapper);
            return await deleter.Delete(id);
        }
    }
}
