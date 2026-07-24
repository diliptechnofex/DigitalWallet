using DigitalWallet.Modules.Wallets.Application.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Modules.Wallets.Presentation.Wallets.MapResults
{
    internal static class ResultProblemMapper
    {
        public static IResult ToProblem<TValue>(this Result<TValue> result, HttpContext httpContext)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot convert a successful result to ProblemDetails.");
            }

            var firstError = result.Errors.First();

            var statusCode = GetStatusCode(firstError.Type);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(firstError.Type),
                Detail = firstError.Message,
                Type = GetProblemType(statusCode),
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

            problemDetails.Extensions["errors"] = result.Errors
                                                        .GroupBy(error => error.Code)
                                                        .ToDictionary(
                                                            group => group.Key,
                                                            group => group
                                                                .Select(error => error.Message)
                                                                .ToArray());

            return Results.Problem(
                detail: problemDetails.Detail,
                statusCode: problemDetails.Status,
                title: problemDetails.Title,
                type: problemDetails.Type,
                instance: problemDetails.Instance,
                extensions: problemDetails.Extensions
            );

        }

        private static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        private static string GetTitle(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => "Validation error",
                ErrorType.NotFound => "Resource not found",
                ErrorType.Conflict => "Conflict",
                ErrorType.Failure => "Server error",
                _ => "Server error"
            };
        }

        private static string GetProblemType(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "https://httpstatuses.com/400",

                StatusCodes.Status404NotFound => "https://httpstatuses.com/404",

                StatusCodes.Status409Conflict => "https://httpstatuses.com/409",

                StatusCodes.Status500InternalServerError => "https://httpstatuses.com/500",

                _ => "https://httpstatuses.com/500"
            };
        }
    }
}
