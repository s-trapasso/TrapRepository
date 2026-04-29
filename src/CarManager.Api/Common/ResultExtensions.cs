using Microsoft.AspNetCore.Mvc;

namespace CarManager.Api.Common
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.Success)
                return new OkObjectResult(result.Data);

            var error = ErrorDetail.From(result.Error);

            return new ObjectResult(new ProblemDetails
            {
                Status = error.HttpStatus,
                Title = error.Code.ToString(),
                Detail = error.Message
            })
            {
                StatusCode = error.HttpStatus
            };
        }
    }
}
