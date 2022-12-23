using System.Globalization;

namespace Cookbook.API.Middleware;

public class CultureMiddleware
{
    private readonly IList<string> _supportedIdioms = new List<string>
    {
        "pt-BR",
        "en-US"
    };

    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        var culture = new CultureInfo("en-US");

        if (context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var language = context.Request.Headers["Accept-Language"].ToString();

            if (_supportedIdioms.Any(c => c.Equals(language)))            
                culture = new CultureInfo(language);
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}
