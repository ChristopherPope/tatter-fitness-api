using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TatterFitness.Bll.Exceptions;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Models.Exercises;

namespace TatterFitness.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exerciseService;

        public ExercisesController(ILogger<ExercisesController> logger, IExercisesService exerciseService)
            : base(logger)
        {
            this.exerciseService = exerciseService;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of all Exercises.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns a list of all Exercises in the system.")]
        public ActionResult<IEnumerable<Exercise>> ReadExercises()
        {
            try
            {
                LogApiCall();

                var exercises = exerciseService.ReadAllExercises();
                LogApiResult(exercises);

                return exercises.ToList();
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read exercises.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The Exercise with the given ExerciseId.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No Exercise with the given ExerciseId exists.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Returns the Exercise with the given ExerciseId.")]
        [Route("{exerciseId}")]
        public ActionResult<Exercise> ReadExercise(int exerciseId)
        {
            try
            {
                LogApiCall();

                var exercise = exerciseService.ReadExercise(exerciseId);
                LogApiResult(exercise);

                return exercise;
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to read exercise {exerciseId}.", exerciseId);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to read exercise {exerciseId}.", exerciseId);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The Exercise with the given ExerciseId was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No Exercise with the given ExerciseId exists.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, BadRequestDescription)]
        [SwaggerOperation("Deletes the Exercise with the given ExerciseId.")]
        [Route("{exerciseId}")]
        public ActionResult DeleteExercise(int exerciseId)
        {
            try
            {
                LogApiCall();

                exerciseService.DeleteExercise(exerciseId);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogError(ex, "Unable to delete exercise {exerciseId}.", exerciseId);
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, "Unable to delete exercise {exerciseId}.", exerciseId);
                return BadRequest(ex.Message);
            }
        }
    }
}
