using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseSetsController : BaseController
    {
        private const string WorkoutOrWorkoutExerciseDoesNotExistDescription = "Either the Workout or WorkoutExercise does not exist.";
        private const string UserNotAuthorizedDescription = "The authenticated user does not have access to the Workout.";
        private const string WorkoutDoesNotExistDescription = "The Workout does not exist.";
        private readonly IWorkoutExerciseSetsService setsSvc;

        public WorkoutExerciseSetsController(ILogger<WorkoutExerciseSetsController> logger, IWorkoutExerciseSetsService setsSvc)
            :base(logger)
        {
            this.setsSvc = setsSvc;
        }

        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status200OK, "The Set has been updated.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status404NotFound, WorkoutOrWorkoutExerciseDoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Update WorkoutExerciseSet.", "The Workout must belong to the authenticated user.")]
        [Route("{workoutExerciseSetId}")]
        public ActionResult<WorkoutExerciseSet> UpdateSet(int workoutExerciseSetId, [FromBody] JsonPatchDocument<WorkoutExerciseSet> patchDoc)
        {
            try
            {
                LogApiCall();
                var set = new WorkoutExerciseSet();
                patchDoc.ApplyTo(set);
                set.Id = workoutExerciseSetId;
                
                set = setsSvc.Update(set);
                LogApiResult(set);

                return set;
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to update set.");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to update set.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to update set.");
                return BadRequest(ex.Message);
            }
            catch (JsonPatchException ex)
            {
                logger.LogError(ex, "Unable to update set.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the newly created WorkoutExercise.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, WorkoutDoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Create a new WorkoutExerciseSet.", "The Workout must belong to the authenticated user.")]
        public ActionResult<WorkoutExerciseSet> Create([FromBody] WorkoutExerciseSet set)
        {
            try
            {
                LogApiCall();
                set = setsSvc.Create(set);
                LogApiResult(set);

                return Created($"WorkoutExerciseSets/{set.Id}", set);
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to create set.");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to create set.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to create set.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The WorkoutExerciseSet has been deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, WorkoutOrWorkoutExerciseDoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Delete a WorkoutExerciseSet.", "The Workout must belong to the authenticated user.")]
        [Route("{workoutExerciseSetId}")]
        public ActionResult Delete(int workoutExerciseSetId)
        {
            try
            {
                LogApiCall();
                setsSvc.Delete(workoutExerciseSetId);
                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to delete set {workoutExerciseSetId}.", workoutExerciseSetId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to delete set {workoutExerciseSetId}.", workoutExerciseSetId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to delete set {workoutExerciseSetId}.", workoutExerciseSetId);
                return BadRequest(ex.Message);
            }
        }
    }
}
