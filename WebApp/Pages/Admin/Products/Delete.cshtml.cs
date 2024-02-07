namespace WebApp.Pages.Admin.Products;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Warehouseman}")]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
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
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
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
                    DatabaseContext.Products.Remove(ProductViewModel);
                    await DatabaseContext.SaveChangesAsync();

                    var successMessage = string.Format(Messages.Deleted, DataDictionary.Product);
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
