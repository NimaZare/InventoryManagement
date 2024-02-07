namespace WebApp.Pages.Admin.Users;

[Authorize(Roles = UserRoles.Administrator)]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    private readonly UserRoles _userRoles = new();

    [BindProperty]
    public User UserViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundUser = from u in DatabaseContext.Users
                                where u.ID == id
                                select u;

                if (foundUser.Any())
                {
                    var user = foundUser.FirstOrDefault()!;
                    UserViewModel = user;
                    ViewData["AllRoles"] = new SelectList(_userRoles.GetRoles);
                }
                else
                {
                    AddPageError(message: Messages.NotFound);
                }
            }
            else
            {
                AddPageError(message: Messages.NotFound);
            }
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foundUser = from u in DatabaseContext.Users
                            where u.ID == id
                            select u;

            if (foundUser.Any())
            {
                var foundedUser = foundUser.FirstOrDefault()!;

                foundedUser.Role = UserViewModel.Role switch
                {
                    "مدیر" => UserRoles.Administrator,
                    "حسابدار" => UserRoles.Accountant,
                    "انباردار" => UserRoles.Warehouseman,
                    "کارشناس فروش" => UserRoles.SalesExpert,
                    "تحصیلدار" => UserRoles.Collector,
                    "کاربر جدید" => UserRoles.NewUser,
                    _ => UserViewModel.Role,
                };

                foundedUser.FirstName = UserViewModel.FirstName;
                foundedUser.LastName = UserViewModel.LastName;
                foundedUser.Username = UserViewModel.Username;
                foundedUser.EmailAddress = UserViewModel.EmailAddress;
                foundedUser.TellNumber = UserViewModel.TellNumber;
                foundedUser.SetUpdateDateTime();

                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.User);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
                return Page();
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                AddPageError(message: Messages.NotFound);
                return Page();
            }
            else
            {
                AddToastError(message: Messages.UnexpectedError);
            }
        }

        return RedirectToPage("./Index");
    }

    private bool UserExists(int? id)
    {
        return DatabaseContext.Users.Any(e => e.ID == id);
    }
}
