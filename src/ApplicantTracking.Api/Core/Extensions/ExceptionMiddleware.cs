using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApplicantTracking.Api.Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = ex.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                });

                await context.Response.WriteAsJsonAsync(errors);
            }
        }
    }
}
