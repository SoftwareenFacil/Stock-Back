using Microsoft.AspNetCore.Mvc;
using Stock_Back.Controllers.UserApiControllers;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;

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
        [Route("api/[controller]/GetUsers/{id?}")]
        public async Task<IActionResult> GetUsers(int? id)
        {
            // Si se proporciona un ID, obtener el usuario específico.
            if (id.HasValue)
            {
                var userGetById = new GetUsers(_context);
                return await userGetById.GetResponseUserById(id.Value);
            }
            // Si no, obtener todos los usuarios.
            else
            {
                var usergetAll = new GetAllUsers(_context);
                return await usergetAll.GetUsers();
            }
        }

        [HttpPost]
        [Route("api/[controller]/InsertUser")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            // Extrae la claim de SuperAdmin del token.
            var isSuperAdmin = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "SuperAdmin")?.Value;
            var add = new AddUser(_context);
            return await add.InsertUser(user, isSuperAdmin);
        }

        [HttpPut]
        [Route("api/[controller]/UpdateUser")]
        public async Task<IActionResult> Put([FromBody] User userEdited)
        {
            var updater = new UpdateUser(_context);
            return await updater.Update(userEdited);
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleter = new DeleteUser(_context);
            return await deleter.Delete(id);
        }
    }
}
