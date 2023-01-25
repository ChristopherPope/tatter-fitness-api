using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseModifiersController : BaseController
    {
        private const string DoesNotExistDescription = "Either the Workout, WorkoutExercise, WorkoutExerciseModifier, or ExerciseModifier does not exist.";
        private const string UserNotAuthorizedDescription = "The authenticated user does not have access to the Workout.";
        private readonly IWorkoutExerciseModifiersService modsSvc;

        public WorkoutExerciseModifiersController(ILogger<WorkoutExerciseModifiersController> logger, IWorkoutExerciseModifiersService modsSvc)
            :base(logger)
        {
            this.modsSvc = modsSvc;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the newly created WorkoutExercise.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, DoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Add an ExerciseModifier to a WorkoutExercise.", "The Workout must belong to the authenticated user.")]
        public ActionResult<WorkoutExerciseModifier> Create([FromBody] WorkoutExerciseModifier mod)
        {
            try
            {
                LogApiCall();
                mod = this.modsSvc.Create(mod);
                LogApiResult(mod);

                return Created($"WorkoutExerciseModifiers/{mod.Id}", mod);
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to create mod.");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to create mod.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to create mod.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The WorkoutExerciseModifier has been deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, DoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Remove an ExerciseModifier from a WorkoutExercise.", "The Workout must belong to the authenticated user.")]
        [Route("{workoutExerciseModifierId}")]
        public ActionResult Delete(int workoutExerciseModifierId)
        {
            try
            {
                LogApiCall();
                this.modsSvc.Delete(workoutExerciseModifierId);
                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to delete mod id {workoutExerciseModifierId}.", workoutExerciseModifierId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to delete mod id {workoutExerciseModifierId}.", workoutExerciseModifierId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to delete mod id {workoutExerciseModifierId}.", workoutExerciseModifierId);
                return BadRequest(ex.Message);
            }
        }
    }
}
