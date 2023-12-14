using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IList<string> _supportedLanguages = new List<string>
    {
        "pt"
    };
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {

        var culture = new CultureInfo("pt");
        if (context.Request.Headers.ContainsKey("accept-language"))
        {
            var language = context.Request.Headers["accept-language"].ToString();

            if (_supportedLanguages.Any(supportedLanguage => supportedLanguage.Equals(language)))
            {
                culture = new CultureInfo(language);
            }
        }
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}
