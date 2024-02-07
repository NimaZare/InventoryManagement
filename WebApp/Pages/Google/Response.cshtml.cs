namespace Server.Pages.Google;

public class ResponseModel : BasePageModel
{
	public async Task<IActionResult> OnGet()
	{
		var result = await HttpContext.AuthenticateAsync(scheme: CookieAuthenticationDefaults.AuthenticationScheme);

		if (result != null && result.Principal != null)
		{
			var claims = result.Principal.Identities
				.FirstOrDefault()?.Claims.Select(claim => new
				{
					claim.Issuer,
					claim.OriginalIssuer,
					claim.Type,
					claim.Value
				});
		}

		return RedirectToPage(pageName: "/Index");
	}
}
