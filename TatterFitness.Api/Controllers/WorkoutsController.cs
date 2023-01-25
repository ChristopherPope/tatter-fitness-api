using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Bll.Services;
using TatterFitness.Models;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : BaseController
    {
        private const string WorkoutDoesNotExistDescription = "The Workout does not exist.";
        private const string UserNotAuthorizedDescription = "The authenticated user does not have access to the Workout.";

        private readonly IWorkoutsService workoutsSvc;

        public WorkoutsController(ILogger<WorkoutsController> logger, IWorkoutsService workoutsSvc)
            : base(logger)
        {
            this.workoutsSvc = workoutsSvc;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the newly created Workout.")]
        [SwaggerOperation("Creates a new Workout under the authenticated user.")]
        public ActionResult<Workout> CreateWorkout([FromBody] Workout workout)
        {
            try
            {
                LogApiCall();
                workout = workoutsSvc.Create(workout);
                LogApiResult(workout);

                return Created($"Workouts/{workout.Id}", workout);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to create workout.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to create workout.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the Workout.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The Workout does not exist.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns the Workout with the given Workout ID.", "The Workout must belong to the authenticated user.")]
        [Route("{workoutId}")]
        public ActionResult<Workout> GetWorkout(int workoutId)
        {
            try
            {
                LogApiCall();

                var workout = workoutsSvc.Read(workoutId);
                LogApiResult(workout);

                return workout;
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to get workout {workoutId}.", workoutId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to get workout {workoutId}.", workoutId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to get workout {workoutId}.", workoutId);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The Workout has been deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The Workout does not exist.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Deletes the Workout with the given Workout ID.", "The Workout must belong to the authenticated user.")]
        [Route("{workoutId}")]
        public ActionResult DeleteWorkout(int workoutId)
        {
            try
            {
                LogApiCall();

                workoutsSvc.Delete(workoutId);
                return Ok();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to delete workout {workoutId}.", workoutId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to delete workout {workoutId}.", workoutId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError("ex, Unable to delete workout {workoutId}.", workoutId);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [SwaggerResponse(204, "The update to the Workout was successfull.")]
        [SwaggerResponse(404, WorkoutDoesNotExistDescription)]
        [SwaggerResponse(401, UserNotAuthorizedDescription)]
        [SwaggerResponse(400, BadRequestDescription)]
        [SwaggerOperation("Update a Workout", "The Workout Id must be owned by the authenticated user.")]
        [Route("{workoutId}")]
        public IActionResult UpdateWorkout(int workoutId, [FromBody] JsonPatchDocument patchDoc)
        {
            try
            {
                LogApiCall();
                var workout = workoutsSvc.Read(workoutId);
                patchDoc.ApplyTo(workout);
                workout.Id = workoutId;
                workoutsSvc.Update(workout);

                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to update workout {workoutId}.", workoutId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to update workout {workoutId}.", workoutId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to update workout {workoutId}.", workoutId);
                return BadRequest(ex.Message);
            }
        }
    }
}
