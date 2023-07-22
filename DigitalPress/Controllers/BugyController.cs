using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressCore.DBContext;
using PressInfrastructure.Errors;

namespace DigitalPress.Controllers
{
    public class BugyController : BaseController
    {
        private readonly PressContext _Context;

        public BugyController(PressContext context)
        {
            _Context = context;
        }

        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _Context.Products.Find("234");
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var thing = _Context.Products.Find("234");
            var thingerr = thing.ToString();
            return Ok();
        }

        [HttpGet("badRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badRequest/{id}")]
        public ActionResult GetBadRequestId(int id)
        {
            return Ok();
        }
    }
}
