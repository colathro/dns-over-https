public static class AcceptValidationExtensions
{
    public static IApplicationBuilder UseAcceptValidation(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AcceptValidation>();
    }
}

public class AcceptValidation
{
    private readonly RequestDelegate _next;

    public AcceptValidation(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.Accept.Count > 0 &&
            context.Request.Headers.Accept.Contains(Constants.AcceptedContentType))
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(Constants.Errors.MissingAcceptHeader);
        }
    }
}