using System;
using System.Net;
using Exterminator.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace TechnicalRadiation.WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature.Error;

                    //var logService = app.ApplicationServices.GetService(typeof(IlogService)) as IlogService;
                    //logService.LogToFile();

                    var statusCode = exception switch
                    {
                        ResourceNotFoundException _ => (int) HttpStatusCode.NotFound,
                        ModelFormatException _ => (int) HttpStatusCode.PreconditionFailed,
                        ArgumentOutOfRangeException _ => (int) HttpStatusCode.BadRequest,
                        _ => (int) HttpStatusCode.InternalServerError
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(new ExceptionModel
                    {
                        StatusCode = statusCode,
                        Message = exception.Message
                    }.ToString());

                });
            });
        }
    }
}