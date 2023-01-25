using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriesController : BaseController
    {
        private readonly IHistoriesService historiesService;

        public HistoriesController(ILogger<HistoriesController> logger, IHistoriesService historiesService)
            : base(logger)
        {
            this.historiesService = historiesService;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a history of the workout events for the current user. ")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Return workout events within the given date range (inclusive).")]
        [Route("Workouts/Events")]
        public ActionResult<IEnumerable<EventDay>> GetWorkoutEvents([FromBody] WorkoutDateRange dateRange)
        {
            try
            {
                LogApiCall();
                var history = historiesService.ReadWorkoutEvents(dateRange);
                LogApiResult(history);

                return history;
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to get workout events for {start:MM/dd/yyyy}-{end:MM/dd/yyyy}.", dateRange.InclusiveFrom, dateRange.InclusiveTo);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the authenticated user's Workouts which fall within the given date range (inclusive).")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Return the authenticated user's Workouts within the given date range (inclusive).")]
        [Route("Workouts")]
        public ActionResult<IEnumerable<Workout>> GetWorkouts([FromBody] WorkoutDateRange dateRange)
        {
            try
            {
                LogApiCall();
                var workouts = historiesService.ReadWorkouts(dateRange).ToList();
                LogApiResult(workouts);

                return workouts;
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to get workout history for {start:MM/dd/yyyy}-{end:MM/dd/yyyy}.", dateRange.InclusiveFrom, dateRange.InclusiveTo);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The authenticated user's Workout history on the Exercise.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The Exercise does not exist.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns the authenticated user's Workout history on the Exercise.")]
        [Route("Exercise/{exerciseId}")]
        public ActionResult<IEnumerable<ExerciseHistory>> GetExerciseHistory(int exerciseId)
        {
            try
            {
                LogApiCall();
                var history = historiesService.ReadByExercise(exerciseId).ToList();
                LogApiResult(history);

                return history;
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Cannot get exercise history for exerciseId: {exerciseId}.", exerciseId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Cannot get exercise history for exerciseId: {exerciseId}.", exerciseId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Cannot get exercise history for exerciseId: {exerciseId}.", exerciseId);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The authenticated user's first and last workout dates.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns the the first and last workout dates for the current user.")]
        [Route("FirstAndLastWorkoutDates")]
        public ActionResult<WorkoutDateRange> GetFirstAndLastWorkoutDates()
        {
            try
            {
                LogApiCall();
                var range = historiesService.ReadFirstAndLastWorkoutDates();
                LogApiResult(range);

                return range;
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Cannot get the first and last workout dates.");
                return BadRequest(ex.Message);
            }
        }
    }
}
