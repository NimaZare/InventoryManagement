namespace WebApp.Pages.Admin.Customers;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Customer CustomerViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundCustomer = from c in DatabaseContext.Customers
                                    where c.ID == id
                                    select c;

                if (foundCustomer.Any())
                {
                    CustomerViewModel = foundCustomer.FirstOrDefault()!;
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
                var foundCustomer = from c in DatabaseContext.Customers
                                    where c.ID == id
                                    select c;

                if (foundCustomer.Any())
                {
                    CustomerViewModel = foundCustomer.FirstOrDefault()!;
                    DatabaseContext.Customers.Remove(CustomerViewModel);
                    await DatabaseContext.SaveChangesAsync();

                    var successMessage = string.Format(Messages.Deleted, DataDictionary.Customer);
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
