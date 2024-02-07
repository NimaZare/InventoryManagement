namespace WebApp.Pages.Admin.Factors;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}, {UserRoles.Accountant}, {UserRoles.Warehouseman}")]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public Factor FactorViewModel { get; set; } = default!;

    public IList<FactorProduct> FactorProductList { get; set; } = [];


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
                .Include(f => f.FactorProducts)
                .ThenInclude(fp => fp.Product)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (factor == null)
            {
                AddPageError(Messages.NotFound);
                return Page();
            }
            else
            {
                FactorViewModel = factor;
                FactorProductList = [.. factor.FactorProducts];
            }
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }

        return Page();
    }
}
