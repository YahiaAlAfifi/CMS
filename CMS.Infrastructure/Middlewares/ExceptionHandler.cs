using CMS.Core.Exceptions;
using CMS.Core.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;
            var response = new JsonResponse();

            context.Response.StatusCode = (int)HttpStatusCode.OK;

            switch (exception)
            {
                case DuplicateEmailOrPhoneException:
                case EntityNotFoundException:
                case OperationFailedException:
                case InvalidDateException:
                    response.msg = $"e:{exception.Message}";
                    response.close = 0;
                    response.status = 0;
                    break;
                default:
                    response.msg = "e:حدث خطأ ما";
                    response.close = 0;
                    response.status = 0;
                    break;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
