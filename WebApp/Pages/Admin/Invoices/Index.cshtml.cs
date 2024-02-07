namespace WebApp.Pages.Admin.Invoices;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Accountant}")]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public IList<Invoice> InvoiceViewModel { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchDebt { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            var invoices = DatabaseContext.Invoices
                .Include(i => i.Customer)
                .OrderByDescending(i => i.UpdateDateTime);

            if (!string.IsNullOrEmpty(SearchDebt))
            {
                invoices = invoices.Where(c => c.Debt.ToString() == SearchDebt)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            InvoiceViewModel = await invoices.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
