namespace WebApp.Pages.Admin.Funds;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public IList<Fund> FundViewModel { get; set; } = new List<Fund>();

    [BindProperty(SupportsGet = true)]
    public DateTime? SearchDateTime { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            var funds = from f in DatabaseContext.Funds
                        orderby f.UpdateDateTime descending
                        select f;

            if (SearchDateTime != null)
            {
                funds = funds.Where(c => c.ChargeDateTime.Date == SearchDateTime)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            FundViewModel = await funds.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
