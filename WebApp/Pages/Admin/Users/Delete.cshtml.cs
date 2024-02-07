namespace WebApp.Pages.Admin.Users;

[Authorize(Roles = UserRoles.Administrator)]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
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
                    UserViewModel = foundUser.FirstOrDefault()!;
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
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
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
                    DatabaseContext.Users.Remove(user);
                    await DatabaseContext.SaveChangesAsync();

                    var successMessage = string.Format(Messages.Deleted, DataDictionary.User);
                    AddToastSuccess(message: successMessage);
                }
                else
                {
                    AddPageError(message: Messages.NotFound);
                    return Page();
                }
            }
            else
            {
                AddPageError(message: Messages.NotFound);
                return Page();
            }
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
