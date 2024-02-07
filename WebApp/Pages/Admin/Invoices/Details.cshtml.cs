namespace WebApp.Pages.Admin.Invoices;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Accountant}")]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public Invoice InvoiceViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundInvoices = from f in DatabaseContext.Invoices
                                    where f.ID == id
                                    select f;

                if (foundInvoices.Any())
                {
                    InvoiceViewModel = foundInvoices.FirstOrDefault()!;
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
