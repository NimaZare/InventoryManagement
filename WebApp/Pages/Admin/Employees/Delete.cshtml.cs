namespace WebApp.Pages.Admin.Employees;

[Authorize(Roles = UserRoles.Administrator)]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
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
                    DatabaseContext.Employees.Remove(EmployeeViewModel);
                    await DatabaseContext.SaveChangesAsync();

                    var successMessage = string.Format(Messages.Deleted, DataDictionary.Employee);
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
