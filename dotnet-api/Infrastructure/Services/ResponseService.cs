using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Application.Services
{
    public class ResponseService : IResponseService
    {

        public static Task GetPatterResponse(HttpContext context, HttpStatusCode status, string message)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)status;
            }
            

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var response = new BaseResponse<Object>
            {
                Success = false,
                Message = message,
                Status = context.Response.StatusCode,
            };


            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        ActionResult<BaseResponse<T>> IResponseService.Ok<T>()
        {
            return new OkObjectResult(new BaseResponse<T>
            {
                Success = true,
                Message = "Success",
                Status = 200,
            });
        }

        ActionResult<BaseResponse<T>> IResponseService.Ok<T>(T data)
        {
            return new OkObjectResult(new BaseResponse<T>
            {
                Success = true,
                Message = "Success",
                Status = 200,
                Data = data
            });
        }
    }
}
