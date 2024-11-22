
using NLog;
using LogLevel = NLog.LogLevel;

namespace PersonAPI.Middleware
{
  public class ExceptionLoggingMiddleware
  {
    private readonly RequestDelegate _next;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public ExceptionLoggingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
     {
      try
      {
        await _next(context);
      }
      catch (Exception ex) 
      {
        var logEventInfo = new LogEventInfo(LogLevel.Error, _logger.Name, ex.Message)
        {
          Exception = ex
        };
        logEventInfo.Properties["url"] = context.Request?.Path + context.Request?.QueryString;

        _logger.Log(logEventInfo);

        throw new Exception(ex.Message);
      }
    }    
  }

}
