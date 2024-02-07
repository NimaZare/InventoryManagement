namespace WebApp.Pages.Admin.Employees;

[Authorize(Roles = UserRoles.Administrator)]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public Employee EmployeeViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundEmployees = from f in DatabaseContext.Employees
                                     where f.ID == id
                                     select f;

                if (foundEmployees.Any())
                {
                    EmployeeViewModel = foundEmployees.FirstOrDefault()!;
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
}
