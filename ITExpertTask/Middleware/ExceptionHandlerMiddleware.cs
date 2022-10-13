using ITExpertTask.Data;
using ITExpertTask.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IO;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ITExpertTask.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var error = new ErrorDetails
                {
                    Message = ex.Message
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }
    }
}
