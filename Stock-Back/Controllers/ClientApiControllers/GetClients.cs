using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.ClientControllers;
using Stock_Back.Controllers.Services;
using Stock_Back.DAL.Context;
using Stock_Back.Models;

namespace Stock_Back.Controllers.ClientApiControllers
{
    public class GetClients
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetClients(AppDbContext context)
        {
            _context = context;
            _responseService = new ResponseService();
        }
        public async Task<IActionResult> GetResponseClients(int id)
        {
            var clientGetter = new GetClientsController(_context);
            var client = await clientGetter.GetClients(id);
            if (client == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no clients." : $"Client with id {id} not found."));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(client, "Success when searching for clients"));
        }
    }
}
