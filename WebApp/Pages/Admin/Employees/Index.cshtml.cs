namespace WebApp.Pages.Admin.Employees;

[Authorize(Roles = UserRoles.Administrator)]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public IList<Employee> EmployeeViewModel { get; set; } = new List<Employee>();

    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            var employees = from c in DatabaseContext.Employees
                            orderby c.UpdateDateTime descending
                            select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                employees = employees.Where(c => c.PersonnelCode.ToString() == SearchString)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            EmployeeViewModel = await employees.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
