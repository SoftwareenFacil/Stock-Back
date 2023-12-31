using System.Text;
using System.Text.RegularExpressions;
using Stock_Back.Models;

namespace Stock_Back.UserJwt
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
        public static bool IsAdmin(string claim)
        {
            if (string.IsNullOrEmpty(claim))
                return false;
            return true;
        }
    }
}
