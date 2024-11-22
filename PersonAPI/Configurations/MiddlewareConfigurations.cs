using PersonAPI.Middleware;

namespace PersonAPI.Configurations
{
  public static class MiddlewareConfigurations
  {
    public static IApplicationBuilder UseExceptionLoggingMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ExceptionLoggingMiddleware>();
    }
    public static IApplicationBuilder UseLanguageMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<LanguageMiddleware>();
    }
  }
}
