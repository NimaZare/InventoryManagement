namespace WebApp.Pages.Account;

public class LoginModel(StoreContext context, IPasswordHasher passwordHasher) : BasePageModelWithDatabaseContext(context)
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    [BindProperty]
    public string ReturnUrl { get; set; }

    [BindProperty]
    public LoginVM ViewModel { get; set; }


    public void OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            var user = await DatabaseContext.Users.FirstOrDefaultAsync(u => u.Username == ViewModel.Username);

            if (user == null)
            {
                AddPageError(message: Messages.InvalidUsernameOrPassword);
                return Page();
            }
            else
            {
                var passResult = _passwordHasher.Verify(user.Password!, ViewModel.Password!);

                if (passResult == false)
                {
                    AddPageError(message: Messages.InvalidUsernameOrPassword);
                    return Page();
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new(type: ClaimTypes.Name, value: user.Username!),
                        new(type: ClaimTypes.Email, value: user.EmailAddress!)
                    };

                    if (user.Role == UserRoles.Administrator)
                    {
                        claims.Add(new Claim(type: ClaimTypes.Role, value: UserRoles.Administrator));
                    }
                    else if (user.Role == UserRoles.Accountant)
                    {
                        claims.Add(new Claim(type: ClaimTypes.Role, value: UserRoles.Accountant));
                    }
                    else if (user.Role == UserRoles.Warehouseman)
                    {
                        claims.Add(new Claim(type: ClaimTypes.Role, value: UserRoles.Warehouseman));
                    }
                    else if (user.Role == UserRoles.SalesExpert)
                    {
                        claims.Add(new Claim(type: ClaimTypes.Role, value: UserRoles.SalesExpert));
                    }
                    else if (user.Role == UserRoles.Collector)
                    {
                        claims.Add(new Claim(type: ClaimTypes.Role, value: UserRoles.Collector));
                    }
                    else
                    {
                        claims.Add(new Claim(type: ClaimTypes.Role, value: UserRoles.NewUser));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims: claims, authenticationType: Constants.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(identity: claimsIdentity);

                    var authenticationProperties = new AuthenticationProperties
                    {
                        IsPersistent = ViewModel.RememberMe
                    };

                    await HttpContext.SignInAsync(Constants.AuthenticationScheme, claimsPrincipal, authenticationProperties);
                }
            }
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }

        if (string.IsNullOrWhiteSpace(ReturnUrl))
        {
            return RedirectToPage(pageName: "/Index");
        }
        else
        {
            return Redirect(url: ReturnUrl);
        }
    }
}
