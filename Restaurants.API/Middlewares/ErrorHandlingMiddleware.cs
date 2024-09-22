
using Microsoft.AspNetCore.Http.HttpResults;
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
            }
            catch (ForbidException fex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access Forbiden.");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong. " + ex.Message);
            }
        }
    }
}
