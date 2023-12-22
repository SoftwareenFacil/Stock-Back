using Microsoft.AspNetCore.Mvc;
using Stock_Back.DAL;
using Stock_Back.DAL.Controller;
using Stock_Back.DAL.Data;
using Stock_Back.DAL.Interfaces;
using Stock_Back.DAL.Models;
using Stock_Back.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Back.Controllers
{
    
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserController _userController;
        public UserApiController(AppDbContext dbContext)
        {
            _userController = new UserController(dbContext);
        }
        // GET: api/<UserApiController>
        [HttpGet]
        [Route("api/[controller]/GetUsers")]
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var users = await _userController.GetAllUsers();
                if (!users.Any())
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type,users));
                }
                return Ok(ResponseHandler.GetAppResponse(type,users));
            }
            catch (Exception ex)    
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }

        // GET api/<UserApiController>/5
        [HttpGet]
        [Route("api/[controller]/GetUserById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var user = await _userController.GetUserById(id);
                if (user == null)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, user));
                }
                return Ok(ResponseHandler.GetAppResponse(type,user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }

        // POST api/<UserApiController>
        [HttpPost]
        [Route("api/[controller]/InsertUser")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                if (await _userController.UserEmailExist(user.Email))
                {
                    type = ResponseType.Failure;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "This email already exists in our records"));
                }
                var inserted = await _userController.InsertUser(user);
                if (inserted == null)
                {
                    type = ResponseType.Failure;
                    return BadRequest(ResponseHandler.GetAppResponse(type, "The user could not be inserted, please make sure you upload the correct format (name, email and password)."));
                }
                return StatusCode(201,ResponseHandler.GetAppResponse(type,inserted));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }

        // PUT api/<UserApiController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateUser")]
        public async Task<IActionResult> Put([FromBody] User userEdited)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var user = await _userController.GetUserById(userEdited.Id);
                if (user == null)
                {
                    type= ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with id {userEdited.Id} not found."));
                }
                var updatedUser = await _userController.UpdateUser(userEdited);
                return Ok(ResponseHandler.GetAppResponse(type, updatedUser));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }

        // DELETE api/<UserApiController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ResponseType type= ResponseType.Success;
                var deleted = await _userController.DeleteUser(id);
                if (!deleted)
                {
                    type = ResponseType.NotFound;
                    return NotFound(ResponseHandler.GetAppResponse(type, $"User with ID {id} not found."));
                }
                return Ok(ResponseHandler.GetAppResponse(type, $"User with ID {id} deleted successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseHandler.GetExceptionResponse(ex)); // Internal Server Error
            }
        }
    }
}
