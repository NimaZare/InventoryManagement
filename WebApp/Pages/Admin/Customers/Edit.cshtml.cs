namespace WebApp.Pages.Admin.Customers;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foundedCustomer = from c in DatabaseContext.Customers
                                  where c.ID == id
                                  select c;

            if (foundedCustomer.Any())
            {
                var customer = foundedCustomer.FirstOrDefault()!;
                customer.FirstName = CustomerViewModel.FirstName;
                customer.LastName = CustomerViewModel.LastName;
                customer.NationalCode = CustomerViewModel.NationalCode;
                customer.TellNumber = CustomerViewModel.TellNumber;
                customer.Address = CustomerViewModel.Address;
                customer.SetUpdateDateTime();

                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.Customer);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
            {
                AddPageError(message: Messages.NotFound);
            }
            else
            {
                AddToastError(message: Messages.UnexpectedError);
            }
        }

        return RedirectToPage("./Index");
    }

    private bool CustomerExists(int? id)
    {
        return DatabaseContext.Customers.Any(e => e.ID == id);
    }
}
