using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TechnicalRadiation.WebApi.Attributes
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private string _TOKEN = "FLdsfasdflksdSAGDKLS!&%(&(%*sdf?sdfas_asd-";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["Token"];
            if (token != _TOKEN)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) 
                    { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}