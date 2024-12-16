using Application.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace Application.Interfaces
{
    public interface IResponseService
    {
        public ActionResult<BaseResponse<T>> Ok<T>(T data);
        public ActionResult<BaseResponse<T>> Ok<T>();
    }
}
