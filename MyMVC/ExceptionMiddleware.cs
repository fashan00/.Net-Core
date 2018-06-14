using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ExceptionMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware (RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke (HttpContext context) {
        try {
            await _next (context);
        } catch (Exception ex) {
            await context.Response
                .WriteAsync ($"{GetType().Name} catch exception. Message: {ex.Message}");
        }
    }
}