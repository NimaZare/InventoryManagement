namespace Server.Pages.Google;

public class LoginModel : BasePageModel
{
	public async Task OnGet()
	{
		var redirectUri = "/google/response";

		var authenticationProperties = new AuthenticationProperties
		{
			RedirectUri = redirectUri
		};

		await HttpContext.ChallengeAsync(
			scheme: Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme,
			properties: authenticationProperties
			);
	}
}
