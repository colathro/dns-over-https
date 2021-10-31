public static class MimeTypeValidationExtensions
{
    public static IApplicationBuilder UseMimeTypeValidation(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MimeTypeValidation>();
    }
}

public class MimeTypeValidation
{
    private readonly RequestDelegate _next;

    public MimeTypeValidation(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method.Equals(Constants.POST, Constants.IgnoreCase))
        {
            if (!string.IsNullOrWhiteSpace(context.Request.ContentType) &&
                context.Request.ContentType.Equals(Constants.AcceptedContentType, Constants.IgnoreCase))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(Constants.Errors.BadContentType);
            }
        }
        else if (context.Request.Method.Equals(Constants.GET, Constants.IgnoreCase))
        {
            await _next(context);
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(Constants.Errors.BadHTTPMethod);
        }
    }
}