using System;
using System.Net;
using TechnicalRadiation.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Exceptions;

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