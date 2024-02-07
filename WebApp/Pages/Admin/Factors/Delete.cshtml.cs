namespace WebApp.Pages.Admin.Factors;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Factor FactorViewModel { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        try
        {
            if (id == null)
            {
                AddPageError(Messages.NotFound);
                return Page();
            }

            var factor = await DatabaseContext.Factors
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (factor == null)
            {
                AddPageError(Messages.NotFound);
                return Page();
            }
            else
            {
                FactorViewModel = factor;
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
            if (id == null)
            {
                AddPageError(Messages.NotFound);
                return Page();
            }

            var factor = await DatabaseContext.Factors.FindAsync(id);

            if (factor != null)
            {
                DatabaseContext.Factors.Remove(factor);
                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Deleted, DataDictionary.Factor);
                AddToastSuccess(message: successMessage);
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
