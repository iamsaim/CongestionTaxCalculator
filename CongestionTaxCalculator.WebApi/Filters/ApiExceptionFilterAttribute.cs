using CongestionTaxCalculator.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CongestionTaxCalculator.WebApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ApiException), HandleApiException },
                { typeof(AlreadyExistsException), HandleAlreadyExistsException },
                { typeof(UpdateNotAllowedException), HandleUpdateNotAllowedException },
                { typeof(DeleteFailureException), HandleDeletionFailedException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError("Exception occurred {0}", context.Exception);

            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }


        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "Invalid Model State"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails()
            {
                Type = "Not Found",
                Title = "The specified resource was not found.",
                Detail = exception?.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }


        private void HandleApiException(ExceptionContext context)
        {
            var exception = context.Exception as ApiException;

            var details = new ProblemDetails()
            {
                Type = "Bad request payload",
                Title = "System is not able to perform this action",
                Detail = exception?.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleAlreadyExistsException(ExceptionContext context)
        {
            var exception = context.Exception as AlreadyExistsException;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = "Already Exists",
                Title = "The specified resource already exists.",
                Detail = exception?.Message,
            };

            context.Result = new ConflictObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUpdateNotAllowedException(ExceptionContext context)
        {
            var exception = context.Exception as AlreadyExistsException;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = "Not allowed to update",
                Title = "The specified resource is not allowed to update.",
                Detail = exception?.Message,
            };

            context.Result = new ConflictObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleDeletionFailedException(ExceptionContext context)
        {
            var exception = context.Exception as DeleteFailureException;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = "Deletion Failed",
                Title = "The specified resource is not able to delete",
                Detail = exception?.Message,
            };

            context.Result = new ConflictObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "Unauthorized"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            context.ExceptionHandled = true;
        }
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "Internal Server Error"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}