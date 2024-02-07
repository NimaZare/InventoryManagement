namespace WebApp.Pages.Admin.Users;

[Authorize(Roles = UserRoles.Administrator)]
public class CreateModel(StoreContext context, IPasswordHasher passwordHasher) : BasePageModelWithDatabaseContext(context)
{
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly UserRoles _userRoles = new();

    [BindProperty]
    public User UserViewModel { get; set; } = default!;


    public IActionResult OnGet()
    {
        try
        {
            ViewData["NewUserRole"] = new SelectList(_userRoles.GetRoles);
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var fixedUsername = Utility.FixText(text: UserViewModel.Username);

            var foundedAnyUsername = await DatabaseContext.Users
                .Where(current => current.Username == fixedUsername)
                .AnyAsync();

            if (foundedAnyUsername)
            {
                var key = $"{nameof(UserViewModel)}.{nameof(UserViewModel.Username)}";
                var errorMessage = string.Format(format: Messages.AlreadyExists, arg0: DataDictionary.Username);
                ModelState.AddModelError(key: key, errorMessage: errorMessage);
                AddPageWarning(message: errorMessage);
                return OnGet();
            }

            var fixedEmailAddress = Utility.FixText(text: UserViewModel.EmailAddress);

            var foundedAnyEmail = await DatabaseContext.Users
                .Where(current => current.EmailAddress == fixedEmailAddress)
                .AnyAsync();

            if (foundedAnyEmail)
            {
                var key = $"{nameof(UserViewModel)}.{nameof(UserViewModel.EmailAddress)}";
                var errorMessage = string.Format(format: Messages.AlreadyExists, arg0: DataDictionary.EmailAddress);
                ModelState.AddModelError(key: key, errorMessage: errorMessage);
                AddPageWarning(message: errorMessage);
                return OnGet();
            }

            var fixedTellNumber = Utility.FixText(text: UserViewModel.TellNumber);

            var foundedAnyTell = await DatabaseContext.Users
                .Where(current => current.TellNumber == fixedTellNumber)
                .AnyAsync();

            if (foundedAnyTell)
            {
                var key = $"{nameof(UserViewModel)}.{nameof(UserViewModel.TellNumber)}";
                var errorMessage = string.Format(format: Messages.AlreadyExists, arg0: DataDictionary.TellNumber);
                ModelState.AddModelError(key: key, errorMessage: errorMessage);
                AddPageWarning(message: errorMessage);
                return OnGet();
            }

            var passHash = _passwordHasher.Hash(UserViewModel.Password!);
            UserViewModel.Password = passHash;

            UserViewModel.Role = UserViewModel.Role switch
            {
                "مدیر" => UserRoles.Administrator,
                "حسابدار" => UserRoles.Accountant,
                "انباردار" => UserRoles.Warehouseman,
                "کارشناس فروش" => UserRoles.SalesExpert,
                "تحصیلدار" => UserRoles.Collector,
                "کاربر جدید" => UserRoles.NewUser,
                _ => UserViewModel.Role
            };

            UserViewModel.SetUpdateDateTime();
            DatabaseContext.Users.Add(UserViewModel);
            await DatabaseContext.SaveChangesAsync();

            var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.User);
            AddToastSuccess(message: successMessage);
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
