using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models.ClientDTO;

namespace Stock_Back.Controllers.ClientApiControllers
{
    [SuperAdminRequired]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientApiController : ControllerBase
    {
        private AppDbContext _context;
        public ClientApiController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients(int id)
        {
            var userGetter = new GetClients(_context);
            return await userGetter.GetResponseClients(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertClient([FromBody] ClientInsertDTO client)
        {
            var add = new InsertClient(_context);
            return await add.Insert(client);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientEditDTO clientEdited)
        {
            var updater = new UpdateClient(_context);
            return await updater.Update(clientEdited);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var deleter = new DeleteClient(_context);
            return await deleter.Delete(id);
        }
    }
}