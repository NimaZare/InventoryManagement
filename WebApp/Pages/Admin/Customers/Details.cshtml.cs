namespace WebApp.Pages.Admin.Customers;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public Customer CustomerViewModel { get; set; } = default!;
    public IList<Factor> FactorsViewModel { get; set; } = [];


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        try
        {
            if (id != null)
            {
				var foundCustomer = await DatabaseContext.Customers
                    .Where(c => c.ID == id)
                    .Include(c => c.Factors)
                    .ThenInclude(f => f.FactorProducts)
					.FirstOrDefaultAsync();

                if (foundCustomer != null)
                {
                    CustomerViewModel = foundCustomer;
                    FactorsViewModel = [.. foundCustomer.Factors];
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
