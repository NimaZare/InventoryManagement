namespace WebApp.Pages.Admin.Customers;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
	public IList<Customer> CustomerViewModel { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public DateTime? SearchDateTime { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? DateTimeStartSearch { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? DateTimeEndSearch { get; set; }


    public async Task OnGetAsync()
	{
		try
		{
			var customers = from c in DatabaseContext.Customers
							orderby c.UpdateDateTime descending
							select c;

            if (SearchDateTime != null)
            {
				customers = customers.Where(c => c.InsertDateTime.Date == SearchDateTime)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            if (DateTimeStartSearch != null && DateTimeEndSearch != null)
            {
                customers = customers.Where(c => c.InsertDateTime.Date >= DateTimeStartSearch && c.InsertDateTime.Date <= DateTimeEndSearch)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            CustomerViewModel = await customers.ToListAsync();
		}
		catch
		{
			AddPageError(message: Messages.UnexpectedError);
		}
	}
}
