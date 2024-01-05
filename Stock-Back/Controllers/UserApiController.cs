using Microsoft.AspNetCore.Mvc;
using Stock_Back.Controllers.UserApiControllers;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.DTO;

namespace Stock_Back.Controllers
{

    [ApiController]
    public class UserApiController : ControllerBase
    {
        private AppDbContext _context;
        public UserApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        [Route("api/[controller]/GetUsers/{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var userGetter = new GetUsers(_context);
            return await userGetter.GetResponseUsers(id);
        }

        [HttpPost]
        [Route("api/[controller]/InsertUser")]
        public async Task<IActionResult> InsertUser([FromBody] UserInsertDTO user)
        {
            var isSuperAdmin = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SuperAdmin")?.Value;
            var add = new AddUser(_context);
            return await add.InsertUser(user, isSuperAdmin);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserEditDTO userEdited)
        {
            var isSuperAdmin = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SuperAdmin")?.Value;
            var updater = new UpdateUser(_context);
            return await updater.Update(userEdited, isSuperAdmin);
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var isSuperAdmin = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SuperAdmin")?.Value;
            var deleter = new DeleteUser(_context);
            return await deleter.Delete(id, isSuperAdmin);
        }
    }
}
