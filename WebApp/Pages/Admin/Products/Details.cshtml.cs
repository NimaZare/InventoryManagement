namespace WebApp.Pages.Admin.Products;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Warehouseman}")]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public Product ProductViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundProduct = from p in DatabaseContext.Products
                                   where p.ID == id
                                   select p;

                if (foundProduct.Any())
                {
                    ProductViewModel = foundProduct.FirstOrDefault()!;
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
            return Page();
        }

        return Page();
    }
}
