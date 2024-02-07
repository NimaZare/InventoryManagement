namespace WebApp.Pages.Admin.Invoices;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Accountant}")]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
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
            var foundInvoices = from f in DatabaseContext.Invoices
                                where f.ID == id
                                select f;

            if (foundInvoices.Any())
            {
                var invoice = foundInvoices.FirstOrDefault()!;
                invoice.Paid = InvoiceViewModel.Paid;
                invoice.CalculateInvoiceDebt();
                invoice.SetUpdateDateTime();

                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.Invoice);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InvoiceExists(id))
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

    private bool InvoiceExists(int? id)
    {
        return DatabaseContext.Invoices.Any(e => e.ID == id);
    }
}
