﻿using Microsoft.AspNetCore.Mvc;

using Stock_Back.Models;
using Stock_Back.BLL.Models;
using Stock_Back.BLL.Controllers.LoginControllers;
using Stock_Back.UserJwt;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Controllers.UserControllers;
using Stock_Back.BLL.Controllers.JwtControllers;

namespace Stock_Back.Controllers.LoginControllers
{
    public class AuthUser : ControllerBase
    {
        private AppDbContext _context;
        private IManejoJwt _manejoJwt;
        public AuthUser(IManejoJwt manejoJwt, AppDbContext context)
        {
            _context = context;
            _manejoJwt = manejoJwt;
        }

        public async Task<IActionResult> Authenticate(UserCredentials credentials)
        {
            ResponseType type = ResponseType.Failure;
            // Asegúrate de que las credenciales proporcionadas sean válidas
            if (credentials == null || string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                return BadRequest(ResponseHandler.GetAppResponse(type, "Invalid Credentials."));
            }
            var loger = new AuthController(_context);
            var token = await loger.Authenticate(_manejoJwt,credentials);
            if (token != null)
            {
                type = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(type, token));

            }
            return Unauthorized(ResponseHandler.GetAppResponse(type, "Unauthorized Credentials"));
        }
    }
}