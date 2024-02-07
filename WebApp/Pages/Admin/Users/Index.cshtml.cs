namespace WebApp.Pages.Admin.Users;

[Authorize(Roles = UserRoles.Administrator)]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public IList<User> UserViewModel { get; set; } = [];

    private readonly UserRoles _userRoles = new();

    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SearchRole { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            ViewData["UserRole"] = new SelectList(_userRoles.GetRoles);

            var users = from c in DatabaseContext.Users
                        orderby c.UpdateDateTime descending
                        select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                users = users.Where(c => c.FullName.Contains(SearchString))
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            if (!string.IsNullOrEmpty(SearchRole))
            {
                SearchRole = SearchRole switch
                {
                    "مدیر" => UserRoles.Administrator,
                    "حسابدار" => UserRoles.Accountant,
                    "انباردار" => UserRoles.Warehouseman,
                    "کارشناس فروش" => UserRoles.SalesExpert,
                    "تحصیلدار" => UserRoles.Collector,
                    "کاربر جدید" => UserRoles.NewUser,
                    _ => SearchRole,
                };

                users = users.Where(c => c.Role == SearchRole)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            UserViewModel = await users.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
