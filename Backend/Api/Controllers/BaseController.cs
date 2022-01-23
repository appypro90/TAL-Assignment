using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateResponse(ResponseModel responseModel)
        {
            return responseModel.ResponseCode switch
            {
                201 => Created(string.Empty, responseModel.ResponseCode),
                409 => Conflict(responseModel.ResponseCode),
                _ => BadRequest(responseModel.Error),
            };
        }
    }
}
