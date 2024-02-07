namespace Server.Pages.Security;

public class RegisterModel(StoreContext context, IPasswordHasher passwordHasher) : BasePageModelWithDatabaseContext(context)
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    [BindProperty]
    public RegisterVM RegisterViewModel { get; set; }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var newUser = new User
            {
                FirstName = RegisterViewModel.FirstName,
                LastName = RegisterViewModel.LastName,
                Role = UserRoles.NewUser,
                Username = RegisterViewModel.Username,
                Password = _passwordHasher.Hash(RegisterViewModel.Password!),
                EmailAddress = RegisterViewModel.EmailAddress,
                TellNumber = RegisterViewModel.TellNumber
            };

            await DatabaseContext.AddAsync(newUser);
            await DatabaseContext.SaveChangesAsync();

            var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.User);
            AddPageSuccess(message: successMessage);

            return Page();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }

        return RedirectToPage(pageName: "/Index");
    }
}
