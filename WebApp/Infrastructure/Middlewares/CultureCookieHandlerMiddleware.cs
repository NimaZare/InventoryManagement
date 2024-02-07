namespace Infrastructure.Middlewares;

public class CultureCookieHandlerMiddleware(RequestDelegate next, Settings.ApplicationSettings applicationSettings)
{
	public readonly static string CookieName = "Culture.Cookie";

	public static string GetCultureNameByCookie(HttpContext httpContext, IList<string> supportedCultureNames)
	{
		if (supportedCultureNames == null || supportedCultureNames.Count == 0)
		{
			return null;
		}

		var cultureName = httpContext.Request.Cookies[key: CookieName];

		if (string.IsNullOrWhiteSpace(cultureName))
		{
			return null;
		}

		if (supportedCultureNames == null || supportedCultureNames.Contains(cultureName) == false)
		{
			return null;
		}

		return cultureName;
	}

	public static void SetCulture(string cultureName)
	{
		if (string.IsNullOrWhiteSpace(cultureName) == false)
		{
			var cultureInfo = new CultureInfo(name: cultureName);
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}
	}

	public static void CreateCookie(HttpContext httpContext, string cultureName)
	{
		var cookieOptions = new CookieOptions
		{
			Path = "/",
			Secure = false,
			HttpOnly = false,
			IsEssential = false,
			MaxAge = null,
			Expires = DateTimeOffset.UtcNow.AddYears(1),
			SameSite = SameSiteMode.Unspecified
		};

		httpContext.Response.Cookies.Delete(key: CookieName);

		if (string.IsNullOrWhiteSpace(cultureName) == false)
		{
			httpContext.Response.Cookies.Append(key: CookieName, value: cultureName, options: cookieOptions);
		}
	}

	private RequestDelegate Next { get; } = next;

	private Settings.ApplicationSettings ApplicationSettings { get; } = applicationSettings;

	public async Task InvokeAsync(HttpContext httpContext)
	{
		var defaultCultureName = ApplicationSettings.CultureSettings.DefaultCultureName;
		var supportedCultureNames = ApplicationSettings.CultureSettings.SupportedCultureNames;
		var currentCultureName = GetCultureNameByCookie (httpContext: httpContext, supportedCultureNames: supportedCultureNames);

		currentCultureName ??= defaultCultureName;
		SetCulture(cultureName: currentCultureName);

		await Next(context: httpContext);
	}
}
