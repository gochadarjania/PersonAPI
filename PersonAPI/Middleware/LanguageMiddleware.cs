using System.Globalization;

namespace PersonAPI.Middleware
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Accept-Language", out var languages))
            {
                var selectedLanguage = languages.ToString().Split(',').FirstOrDefault();
                if (!string.IsNullOrEmpty(selectedLanguage))
                {
                    var culture = new CultureInfo(selectedLanguage);
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
            }

            await _next(context);
        }
    }
}
