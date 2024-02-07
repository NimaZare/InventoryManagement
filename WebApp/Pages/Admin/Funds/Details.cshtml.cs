namespace WebApp.Pages.Admin.Funds;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
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
}
