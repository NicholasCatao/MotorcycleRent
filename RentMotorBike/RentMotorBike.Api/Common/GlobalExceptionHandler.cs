using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RentMotorBike.Domain.Response.Base;
using System.Net.Mime;
using System.Text.Json;

namespace RentMotorBike.Api.Common;

public class GlobalExceptionHandler 
{

    private readonly RequestDelegate _next;
    private readonly IOptions<JsonOptions> _jsonOptions;

    public GlobalExceptionHandler(RequestDelegate next, IOptions<JsonOptions> jsonOptions)
    {
        _next = next;
        _jsonOptions = jsonOptions;
    }

    public async Task Invoke(HttpContext context, IWebHostEnvironment hostingEnvironment, ILogger<GlobalExceptionHandler> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, hostingEnvironment, logger, DateTime.Now);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment hostingEnvironment, ILogger<GlobalExceptionHandler> logger, DateTime dtNow)
    {
        logger.LogError(ex, "log-{dtNow}", dtNow);

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new Notificacao()
        {
            DetalheErro = hostingEnvironment.IsProduction() ? "Ocorreu um erro interno ao processar os dados" : ex.Message
        }, _jsonOptions.Value.JsonSerializerOptions));
    }
}
