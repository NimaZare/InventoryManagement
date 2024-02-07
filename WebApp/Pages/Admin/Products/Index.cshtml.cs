namespace WebApp.Pages.Admin.Products;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Warehouseman}, {UserRoles.SalesExpert}")]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public IList<Product> ProductViewModel { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? EntryDateTimeSearch { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? LeavingTheWarehouseDateTimeSearch { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            var products = from c in DatabaseContext.Products
                           orderby c.UpdateDateTime descending
                           select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(c => c.ProductName.Contains(SearchString))
                           .OrderByDescending(c => c.UpdateDateTime);
            }

            if (EntryDateTimeSearch != null)
            {
                products = products.Where(c => c.EntryDateTime.Date == EntryDateTimeSearch)
                           .OrderByDescending(c => c.UpdateDateTime);
            }

            if (LeavingTheWarehouseDateTimeSearch != null)
            {
                products = products.Where(c => c.LeavingTheWarehouseDateTime.Date == LeavingTheWarehouseDateTimeSearch)
                           .OrderByDescending(c => c.UpdateDateTime);
            }

            ProductViewModel = await products.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
