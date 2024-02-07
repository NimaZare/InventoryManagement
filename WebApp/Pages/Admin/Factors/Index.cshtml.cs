namespace WebApp.Pages.Admin.Factors;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}, {UserRoles.Accountant}, {UserRoles.Warehouseman}")]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public List<Factor> FactorViewModel { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    [MaxLength(10)]
    public string SearchString { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? SearchDateTime { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            var factors = DatabaseContext.Factors
                .Include(f => f.Customer)
                .Include(fp => fp.FactorProducts)
                .OrderByDescending(f => f.UpdateDateTime);

            if (!string.IsNullOrEmpty(SearchString))
            {
                factors = factors.Where(c => c.Customer.NationalCode == SearchString)
                    .OrderByDescending(f => f.UpdateDateTime);
            }

            if (SearchDateTime != null)
            {
                factors = factors.Where(c => c.Customer.InsertDateTime.Date == SearchDateTime)
                    .OrderByDescending(f => f.UpdateDateTime);
            }

            FactorViewModel = await factors.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
