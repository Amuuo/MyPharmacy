using Microsoft.AspNetCore.Http.Extensions;

namespace PharmacyApi.Middleware
{
    public class RequestExceptionMiddleware
    {
        #region Members and Constructor

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestExceptionMiddleware> _logger;

        public RequestExceptionMiddleware(RequestDelegate next, 
                                          ILogger<RequestExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong when handling the request {@request}", 
                                 context.Request.GetDisplayUrl());
                
                await HandleExceptionAsync(context);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync("An unexpected error occured");
        }
    }
}
