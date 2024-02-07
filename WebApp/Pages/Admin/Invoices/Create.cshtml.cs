namespace WebApp.Pages.Admin.Invoices;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Accountant}")]
public class CreateModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Invoice InvoiceViewModel { get; set; } = default!;

    [BindProperty]
    public int CustomerIdViewModel { get; set; }


    public IActionResult OnGet()
    {
        ViewData["CustomerId"] = new SelectList(DatabaseContext.Customers.OrderByDescending(c => c.UpdateDateTime), "ID", "FullName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var list = DatabaseContext.Factors
                .Where(f => f.CustomerID == InvoiceViewModel.CustomerID)
                .ToList();
            InvoiceViewModel.Customer = DatabaseContext.Customers.FirstOrDefault(c => c.ID == InvoiceViewModel.CustomerID)!;
            InvoiceViewModel.FactorsTotalAmount = list.Sum(f => f.TotalAmount);
            InvoiceViewModel.CalculateInvoiceDebt();

            DatabaseContext.Invoices.Add(InvoiceViewModel);
            await DatabaseContext.SaveChangesAsync();

            var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.Invoice);
            AddToastSuccess(message: successMessage);
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }

        return RedirectToPage("./Index");
    }
}
