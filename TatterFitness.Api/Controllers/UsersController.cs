using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsersService userService;

        public UsersController(ILogger<UsersController> logger, IUsersService dataService)
            : base(logger)
        {
            this.userService = dataService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        public ActionResult<User> Get()
        {
            try
            {
                LogApiCall();
                var user = userService.GetUser();
                LogApiResult(user);

                return user;
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to get user");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to get user");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, $"Unable to get user.");
                return BadRequest(ex.Message);
            }
        }
    }
}
