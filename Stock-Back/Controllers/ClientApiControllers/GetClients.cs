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

        public async Task<IActionResult> GetBy(int? id, string? name, string? email, string? taxId, DateTime? created, bool? vigency)
        {
            var clientGetter = new GetClientsController(_context);
            var clients = await clientGetter.GetClientBy(id, name, email, taxId, created, vigency);
            if (clients.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(clients, "Success when searching for clients"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse("There are no clients with these parameters"));
        }
    }
}
