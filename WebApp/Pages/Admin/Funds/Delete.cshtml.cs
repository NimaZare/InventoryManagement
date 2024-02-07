namespace WebApp.Pages.Admin.Funds;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Fund FundViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundFund = from f in DatabaseContext.Funds
                                where f.ID == id
                                select f;

                if (foundFund.Any())
                {
                    FundViewModel = foundFund.FirstOrDefault()!;
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
                var foundFund = from f in DatabaseContext.Funds
                                where f.ID == id
                                select f;

                if (foundFund.Any())
                {
                    FundViewModel = foundFund.FirstOrDefault()!;
                    DatabaseContext.Funds.Remove(FundViewModel);
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
