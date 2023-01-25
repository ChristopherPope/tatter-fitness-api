using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExercisesController : BaseController
    {
        private const string UserNotAuthorizedDescription = "The authenticated user does not have access to the Workout.";
        private const string WorkoutDoesNotExistDescription = "The Workout or the Exercise does not exist.";
        private const string WorkoutExerciseDoesNotExistDescription = "The WorkoutExercise does not exist.";
        private readonly IWorkoutExercisesService exercisesSvc;

        public WorkoutExercisesController(ILogger<WorkoutExercisesController> logger, IWorkoutExercisesService exercisesSvc)
            : base(logger)
        {
            this.exercisesSvc = exercisesSvc;
        }

        [HttpPatch]
        [SwaggerResponse(204, "The update to the WorkoutExercise was successfull.")]
        [SwaggerResponse(404, WorkoutExerciseDoesNotExistDescription)]
        [SwaggerResponse(401, UserNotAuthorizedDescription)]
        [SwaggerResponse(400, BadRequestDescription)]
        [SwaggerOperation("Update a WorkoutExercise", "The WorkoutExercise must be owned by the authenticated user.")]
        [Route("{workoutExerciseId}")]
        public IActionResult Update(int workoutExerciseId, [FromBody] JsonPatchDocument patchDoc)
        {
            try
            {
                LogApiCall();
                var workoutExercise = exercisesSvc.Read(workoutExerciseId);
                patchDoc.ApplyTo(workoutExercise);
                workoutExercise.Id = workoutExerciseId;
                exercisesSvc.Update(workoutExercise);

                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to update workout exercise {workoutExerciseId}.", workoutExerciseId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to update workout exercise {workoutExerciseId}.", workoutExerciseId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to update workout exercise {workoutExerciseId}.", workoutExerciseId);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the most recent WorkoutExercises for the given list of ExerciseIds.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "An Exercise does not exist.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns the most recent WorkoutExercise for a given list ExerciseIds.")]
        [Route("MostRecent")]
        public ActionResult<IEnumerable<WorkoutExercise>> ReadMostRecent([FromQuery] IEnumerable<int> exerciseIds)
        {
            try
            {
                LogApiCall();
                var workoutExercises = exercisesSvc.ReadMostRecent(exerciseIds).ToList();
                LogApiResult(workoutExercises);

                return workoutExercises;
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to read most recent exercises");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read most recent exercises");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the newly created WorkoutExercise.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, WorkoutDoesNotExistDescription)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Create a new WorkoutExercise.", "The Workout must belong to the authenticated user.")]
        public ActionResult<WorkoutExercise> Create(WorkoutExercise we)
        {
            try
            {
                LogApiCall();
                we = exercisesSvc.Create(we);
                LogApiResult(we);

                return Created($"WorkoutExercises/{we.Id}", we);
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to read most create exercise.");
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to read most create exercise.");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read most create exercise.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The WorkoutExercise has been deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Either the Workout does not exist or the Exercise does not exist on the Workout.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, UserNotAuthorizedDescription)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Delete a WorkoutExercise.", "The Workout must belong to the authenticated user.")]
        [Route("{workoutExerciseId}")]
        public ActionResult RemoveExerciseFromWorkout(int workoutExerciseId)
        {
            try
            {
                LogApiCall();
                exercisesSvc.Delete(workoutExerciseId);
                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to read most remove exercise from workout {workoutExerciseId}.", workoutExerciseId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to read most remove exercise from workout {workoutExerciseId}.", workoutExerciseId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read most remove exercise from workout {workoutExerciseId}.", workoutExerciseId);
                return BadRequest(ex.Message);
            }
        }
    }
}
