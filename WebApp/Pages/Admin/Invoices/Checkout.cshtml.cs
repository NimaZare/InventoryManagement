namespace WebApp.Pages.Admin.Invoices;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Accountant}")]
public class CheckoutModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Invoice InvoiceViewModel { get; set; } = default!;

    [BindProperty]
    public bool HasDebt { get; set; }


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                InvoiceViewModel = DatabaseContext.Invoices
                    .Where(i => i.ID == id)
                    .Include(i => i.Customer)
                    .OrderByDescending(i => i.UpdateDateTime)
                    .FirstOrDefault();

                if (InvoiceViewModel.Debt > 0)
                {
                    HasDebt = true;
                    AddPageWarning(message: Messages.UserHasDebt);
                    return Page();
                }
                else
                {
                    HasDebt = false;
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
