using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected const string BadRequestDescription = "Bad request.";
        protected ILogger logger;

        public BaseController(ILogger logger)
        {
            this.logger = logger;
        }

        protected void LogApiCall()
        {
            logger.LogInformation($"{Request.Method} {Request.Path}");
        }

        protected void LogApiResult(object result)
        {
            logger.LogInformation(JsonConvert.SerializeObject(result, Formatting.Indented));
        }
    }
}
