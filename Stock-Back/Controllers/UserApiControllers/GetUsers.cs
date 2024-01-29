using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Context;
using Stock_Back.BLL.Models;
using Stock_Back.Models;
using Stock_Back.Controllers.Services;

namespace Stock_Back.Controllers.UserApiControllers
{
    public class GetUsers : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ResponseService _responseService;
        public GetUsers(AppDbContext dbContext)
        {
            _context = dbContext;
            _responseService = new ResponseService();

        }

        public async Task<IActionResult> GetBy(int? id, string? name, string? email, DateTime? created, bool? vigency)
        {
            var userGetter = new GetUsersController(_context);
            var users = await userGetter.GetUsersBy(id, name, email, created, vigency);
            if (users.Count() > 0)
            {
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(users, "Success when searching for users"));
            }
            return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse("There are no users with these parameters"));
        }
    }
}
