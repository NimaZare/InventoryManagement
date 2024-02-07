namespace WebApp.Pages.Admin.Products;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Warehouseman}")]
public class CreateModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Product ProductViewModel { get; set; } = default!;

    private readonly ProductCategories _productCategories = new();


    public IActionResult OnGet()
    {
        try
        {
            ViewData["ProductCategory"] = new SelectList(_productCategories.GetCategories);
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }

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

            ProductViewModel.SetUpdateDateTime();

            ProductViewModel.Category = ProductViewModel.Category switch
            {
                "مواد ویتامینی" => ProductCategories.Vitamins,
                "مواد معدنی" => ProductCategories.Minerals,
                _ => ProductViewModel.Category
            };

            DatabaseContext.Products.Add(ProductViewModel);
            await DatabaseContext.SaveChangesAsync();

            var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.Product);
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
