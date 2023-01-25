using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutinesController : BaseController
    {
        private const string RoutineDoesNotExistDescription = "The Routine does not exist.";
        private const string UserNotAuthorizedDescription = "The authtenticated user does not have access to the Routine.";
        private readonly IRoutinesService routineService;

        public RoutinesController(ILogger<RoutinesController> logger, IRoutinesService routineService)
            : base(logger)
        {
            this.routineService = routineService;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Return the Routines for the current authorized user.")]
        public ActionResult<IEnumerable<Routine>> ReadRoutines()
        {
            try
            {
                LogApiCall();
                var routines = routineService.ReadRoutines();
                LogApiResult(routines);

                return routines.ToList();
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read routines.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The new Routine has been created.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Create a new Routine.", "The new Routine will be associated with the authetnicated user.")]
        public ActionResult<Routine> CreateRoutine([FromBody] Routine routine)
        {
            try
            {
                LogApiCall();
                routine = routineService.CreateRoutine(routine);
                LogApiResult(routine);

                return Created($"Routines/{routine.Id}", routine);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to create a new Routine.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerResponse(200, "The Routine with the given Routine ID is returned.")]
        [SwaggerResponse(404, RoutineDoesNotExistDescription)]
        [SwaggerResponse(401, UserNotAuthorizedDescription)]
        [SwaggerResponse(400, BadRequestDescription)]
        [SwaggerOperation("Return a Routine", "The Routine Id must be owned by the authenticated user.")]
        [Route("{routineId}")]
        public ActionResult<Routine> ReadRoutine(int routineId)
        {
            try
            {
                LogApiCall();

                var routine = routineService.ReadRoutine(routineId);
                LogApiResult(routine);

                return routine;
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to read routine {routineId}.", routineId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to read routine {routineId}.", routineId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read routine {routineId}.", routineId);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [SwaggerResponse(204, "The update to the Routine was successfull.")]
        [SwaggerResponse(404, RoutineDoesNotExistDescription)]
        [SwaggerResponse(401, UserNotAuthorizedDescription)]
        [SwaggerResponse(400, BadRequestDescription)]
        [SwaggerOperation("Update a Routine", "The Routine Id must be owned by the authenticated user.")]
        [Route("{routineId}")]
        public IActionResult UpdateRoutine(int routineId, [FromBody] JsonPatchDocument patchDoc)
        {
            try
            {
                LogApiCall();
                var routine = routineService.ReadRoutine(routineId);
                patchDoc.ApplyTo(routine);
                routine.Id = routineId;
                routineService.UpdateRoutine(routine);

                return NoContent();
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to update routine {routineId}.", routineId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to update routine {routineId}.", routineId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to update routine {routineId}.", routineId);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(204, "The Routine was deleted successfully.")]
        [SwaggerResponse(404, RoutineDoesNotExistDescription)]
        [SwaggerResponse(401, UserNotAuthorizedDescription)]
        [SwaggerResponse(400, BadRequestDescription)]
        [SwaggerOperation("Delete a Routine", "The Routine Id must be owned by the authenticated user.")]
        [Route("{routineId}")]
        public IActionResult DeleteRoutine(int routineId)
        {
            try
            {
                LogApiCall();

                routineService.DeleteRoutine(routineId);
            }
            catch (UserNotAuthorizedException ex)
            {
                logger.LogError(ex, "Unable to delete routine {routineId}.", routineId);
                return Unauthorized(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to delete routine {routineId}.", routineId);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to delete routine {routineId}.", routineId);
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
