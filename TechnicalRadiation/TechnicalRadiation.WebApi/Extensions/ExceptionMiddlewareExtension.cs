using System;
using System.Net;
using Exterminator.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace TechnicalRadiation.WebApi.ExceptionMiddlewareExtensions
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
                    var statusCode = (int) HttpStatusCode.InternalServerError;

                    //var logService = app.ApplicationServices.GetService(typeof(IlogService)) as IlogService;
                    //logService.LogToFile();

                    if (exception is ResourceNotFoundException)
                    {
                        statusCode = (int)HttpStatusCode.NotFound;
                    }
                    else if (exception is ModelFormatException)
                    {
                        statusCode = (int)HttpStatusCode.PreconditionFailed;
                    }

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