namespace WebApp.Pages.Admin.Users;

[Authorize(Roles = UserRoles.Administrator)]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
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
            return Page();
        }

        return Page();
    }
}
