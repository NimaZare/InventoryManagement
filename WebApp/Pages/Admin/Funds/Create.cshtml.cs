namespace WebApp.Pages.Admin.Funds;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class CreateModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Fund FundViewModel { get; set; } = default!;


    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DatabaseContext.Funds.Add(FundViewModel);
            await DatabaseContext.SaveChangesAsync();

            var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.Fund);
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
