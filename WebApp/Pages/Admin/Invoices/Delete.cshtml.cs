namespace WebApp.Pages.Admin.Invoices;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Accountant}")]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
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
                    DatabaseContext.Invoices.Remove(InvoiceViewModel);
                    await DatabaseContext.SaveChangesAsync();

                    var successMessage = string.Format(Messages.Deleted, DataDictionary.Fund);
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
