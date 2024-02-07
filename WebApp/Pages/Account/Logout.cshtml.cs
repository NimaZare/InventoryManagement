namespace Server.Pages.Account;

[Authorize]
public class LogoutModel : BasePageModel
{
	public async Task<IActionResult> OnGet()
	{
		await HttpContext.SignOutAsync(scheme: Constants.AuthenticationScheme);
		return RedirectToPage(pageName: "/Index");
	}
}
