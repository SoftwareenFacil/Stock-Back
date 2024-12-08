﻿using EIC_Back.BLL.Models;

namespace EIC_Back.UserJwt
{
    public class ResponseHandler
    {
        public static ApiResponse GetExceptionResponse(Exception ex)
        {
            ApiResponse response = new ApiResponse();
            response.Message = ex.Message;
            return response;
        }

        public static ApiResponse GetAppResponse(ResponseType type, object? contract)
        {
            ApiResponse response;

            response = new ApiResponse { ResponseData = contract };
            switch (type)
            {
                case ResponseType.Success:
                    response.Message = "Success";
                    break;
                case ResponseType.NotFound:
                    response.Message = "No record available";
                    break;
                case ResponseType.Failure:
                    response.Message = "Operation rejected";
                    break;
            }
            return response;
        }
    }
}
