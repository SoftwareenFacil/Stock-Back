using Microsoft.AspNetCore.Mvc;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Models.DTO;
using Stock_Back.BLL.Controllers.UserControllers;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Context;


namespace Stock_Back.Controllers.UserApiControllers
{
    public class AddUser : ControllerBase
    {
        private readonly AppDbContext _context;
        public AddUser(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> InsertUser(UserInsertDTO user)
        {

                try
                {
                    ResponseType type = ResponseType.Failure;
                    var userCreator = new AddUsersController(_context);
                    var (isUser, userExist) = await userCreator.AddUser(user);
                    if (isUser)
                    {
                        type = ResponseType.Success;
                        return StatusCode(201, ResponseHandler.GetAppResponse(type, user));
                    }
                    
                    if (userExist)
                    {
                        return BadRequest(ResponseHandler.GetAppResponse(type, "This email already exists in our records"));
                    }

                    return BadRequest(ResponseHandler.GetAppResponse(type, "The user could not be inserted, please make sure you upload the correct format (name, email, phone and password)."));
                    
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ResponseHandler.GetExceptionResponse(ex));
                }
            }
        }
    }

