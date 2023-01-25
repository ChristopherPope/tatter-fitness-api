using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models.Exercises;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineExercisesController : BaseController
    {
        private const string RoutineExerciseDoesNotExistDescription = "The RoutineExercise does not exist.";
        private const string UserNotAuthorizedDescription = "The authtenticated user does not have access to the RoutineExercise.";
        private readonly IRoutineExercisesService routineExService;

        public RoutineExercisesController(ILogger<RoutinesController> logger, IRoutineExercisesService routineExService)
            : base(logger)
        {
            this.routineExService = routineExService;
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent, "All Exercises have been removed from the Routine.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Either the Routine does not exist or one of the Exercise ids does not exist on that Routine.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Delete a list of Exercises from the Routine.")]
        [Route("{routineId}")]
        public IActionResult Delete(int routineId, [FromQuery] IEnumerable<int> exerciseIds)
        {
            try
            {
                LogApiCall();
                routineExService.Delete(routineId, exerciseIds);
                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to delete routine exercises.");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to delete routine exercises.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to delete routine exercises.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of the newly created RoutineExercises.")]
        [SwaggerResponse(StatusCodes.Status303SeeOther, "The RoutineExercise already exists.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, RoutineExerciseDoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Adds the exercises to the routine.")]
        [Route("{routineId}")]
        public ActionResult<IEnumerable<RoutineExercise>> Create(int routineId, [FromBody] IEnumerable<int> exerciseIds)
        {
            try
            {
                LogApiCall();
                var exercises = routineExService.Create(routineId, exerciseIds).ToList();
                LogApiResult(exercises);

                return exercises;
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to create routine exercises.");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to create routine exercises.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to create routine exercises.");
                return BadRequest(ex.Message);
            }
            catch (EntityAlreadyExistsException ex)
            {
                logger.LogError(ex, "Unable to create routine exercises.");
                return StatusCode(StatusCodes.Status303SeeOther, ex.Message);
            }
        }
    }
}
