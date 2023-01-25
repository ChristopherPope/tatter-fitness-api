using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models.Exercises;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseModifiersController : BaseController
    {
        private readonly IExerciseModifiersService exerciseModSvc;

        public ExerciseModifiersController(ILogger<ExerciseModifiersController> logger, IExerciseModifiersService exerciseModSvc)
            : base(logger)
        {
            this.exerciseModSvc = exerciseModSvc;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of all ExerciseModifiers.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns a list of all ExerciseModifiers in the system.")]
        public ActionResult<IEnumerable<ExerciseModifier>> ReadExerciseModifications()
        {
            try
            {
                LogApiCall();
                var mods = exerciseModSvc.ReadModifiers().ToList();
                LogApiResult(mods);

                return mods;
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read exercise mods.");
                return BadRequest(ex.Message);
            }
        }
    }
}
