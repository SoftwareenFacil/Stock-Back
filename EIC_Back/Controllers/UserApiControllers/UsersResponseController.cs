using Microsoft.AspNetCore.Mvc;
using EIC_Back.DAL.Context;
using EIC_Back.Models;
using EIC_Back.Controllers.Services;
using AutoMapper;
using EIC_Back.BLL.Models.UserModelDTO;
using EIC_Back.BLL.Services;
using Microsoft.EntityFrameworkCore;

namespace EIC_Back.Controllers.UserApiControllers
{
    public class UsersResponseController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly ResponseService _responseService;
        public UsersResponseController(AppDbContext dbContext, IMapper mapper)
        {
            _userService = new UserService(dbContext, mapper);
            _responseService = new ResponseService();
            

        }

        public async Task<IActionResult> GetResponseUsers(int id)
        {
            var user = await _userService.GetUsers(id);
            if (user == null)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse(
                id == 0 ? "There are no users" : $"User with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(user, "Success when searching for users"));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _userService.DeleteUserById(id);
            if (!isDeleted)
            {
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with id {id} not found"));

            }
            return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User with ID {id} deleted successfully", "Delete completed"));

        }
        public async Task<IActionResult> Insert(UserInsertDTO user)
        {
            var dataModified = await _userService.AddUser(user);

            if (dataModified > 0)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse(dataModified, $"User created succesfully Create completed"));
            else if (dataModified < 0)
                return _responseService.CreateResponse(ApiResponse<object>.BadRequest(user, $"User with Email {user.Email} already exists"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to Insert User"));

        }
        public async Task<IActionResult> Update(UserEditDTO userEdited)
        {
            var (isUpdated, isUser) = await _userService.UpdateUser(userEdited);

            if (isUpdated)
                return _responseService.CreateResponse(ApiResponse<object>.SuccessResponse($"User with ID {userEdited.Id} updated", "Update completed"));
            else if (!isUser)
                return _responseService.CreateResponse(ApiResponse<object>.NotFoundResponse($"User with ID {userEdited.Id} not found"));
            return _responseService.CreateResponse(ApiResponse<object>.ErrorResponse("Error trying to update User"));
        }
    }
}
