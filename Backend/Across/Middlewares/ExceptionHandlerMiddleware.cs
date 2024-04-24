using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Across.Middlewares.Dto;
using Infrastructure.SmsGateway.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UseCases.Exceptions;

namespace Across.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            //если будет разрастаться, то прикрутить сюда фабрику или что-то в таком духе
            //чтобы изолировать switch case!!!
            httpContext.Response.ContentType = "application/json";
            switch (exception)
            {
                case NotAuthorizedException exc:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    var json = JsonConvert.SerializeObject(new NotAuthorizedDto()
                    {
                        ErrorCode = exc.ErrorCode,
                        AuthorizationMessage = exc.AuthorizationMessage,
                        ExceptionMessage = exc.Message
                    }, Formatting.Indented);
                    await httpContext.Response.WriteAsync(json);
                    break;
                case SecurityTokenExpiredException exc:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    var tokenExpiredDto = JsonConvert.SerializeObject(new NotAuthorizedDto()
                    {
                        ErrorCode = NotAuthorizedErrorCode.AccessTokenExpired,
                        AuthorizationMessage = "Access token expired",
                        ExceptionMessage = exc.Message
                    }, Formatting.Indented);
                    await httpContext.Response.WriteAsync(tokenExpiredDto);
                    break;
                    break;
                case SendSmsErrorException exc:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await httpContext.Response.WriteAsync($"Sms Gateway Status Code: {exc.StatusCode}, Message: {exception.Message}");
                    break;
                case ValidationErrorException exc:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await httpContext.Response.WriteAsync($"Validating error: {exception.Message}");
                    break;
                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await httpContext.Response.WriteAsync(exception.Message);
                    break;
            }

            
        }
    }
}
