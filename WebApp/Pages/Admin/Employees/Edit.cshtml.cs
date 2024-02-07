namespace WebApp.Pages.Admin.Employees;

[Authorize(Roles = UserRoles.Administrator)]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foundEmployees = from f in DatabaseContext.Employees
                                 where f.ID == id
                                 select f;

            if (foundEmployees.Any())
            {
                var employee = foundEmployees.FirstOrDefault()!;
                employee.PersonnelCode = EmployeeViewModel.PersonnelCode;
                employee.FirstName = EmployeeViewModel.FirstName;
                employee.LastName = EmployeeViewModel.LastName;
                employee.BirthDateTime = EmployeeViewModel.BirthDateTime;
                employee.NationalCode = EmployeeViewModel.NationalCode;
                employee.TellNumber = EmployeeViewModel.TellNumber;
                employee.Address = EmployeeViewModel.Address;
                employee.PostCode = EmployeeViewModel.PostCode;
                employee.PersonnelEntryDateTime = EmployeeViewModel.PersonnelEntryDateTime;
                employee.InsuranceCode = EmployeeViewModel.InsuranceCode;
                employee.SetUpdateDateTime();

                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.Employee);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmployeeExists(id))
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

    private bool EmployeeExists(int? id)
    {
        return DatabaseContext.Employees.Any(e => e.ID == id);
    }
}
