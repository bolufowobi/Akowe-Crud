using akowe_CRUD.Controllers;
using akowe_CRUD.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EpumpOBDApi.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiError(code));
        }
    }
}
