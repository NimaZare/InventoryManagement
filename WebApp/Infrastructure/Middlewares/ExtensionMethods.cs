namespace Infrastructure.Middlewares;

public static class ExtensionMethods
{
	public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
	{
		return app.UseMiddleware<CultureCookieHandlerMiddleware>();
	}
}
