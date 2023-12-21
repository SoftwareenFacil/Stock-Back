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
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<User> data = await _userController.GetAllUsers();
                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<UserApiController>/5
        [HttpGet]
        [Route("api/[controller]/GetUserById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                User? data = await _userController.GetUserById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
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
                await _userController.InsertUser(user);
                return Ok(ResponseHandler.GetAppResponse(type, user));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<UserApiController>/5
        [HttpPut]
        [Route("api/[controller]/UpdateUser")]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var response = await _userController.UpdateUser(user);
                return Ok(ResponseHandler.GetAppResponse(type, response));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<UserApiController>/5
        [HttpDelete]
        [Route("api/[controller]/DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                await _userController.DeleteUser(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
