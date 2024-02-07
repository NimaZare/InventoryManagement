using WebApp.Models;

namespace WebApp.Pages.Admin.Products;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Warehouseman}")]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Product ProductViewModel { get; set; } = default!;

    private readonly ProductCategories _productCategories = new();


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
                    var product = foundProduct.FirstOrDefault()!;
                    ProductViewModel = product;
                    ViewData["ProductCategory"] = new SelectList(_productCategories.GetCategories);
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foundProduct = from p in DatabaseContext.Products
                               where p.ID == id
                               select p;

            if (foundProduct.Any())
            {
                var foundedProduct = foundProduct.FirstOrDefault()!;

                foundedProduct.Category = ProductViewModel.Category switch
                {
                    "مواد ویتامینی" => ProductCategories.Vitamins,
                    "مواد معدنی" => ProductCategories.Minerals,
                    _ => ProductViewModel.Category
                };

                foundedProduct.Count = ProductViewModel.Count;
                foundedProduct.ProductName = ProductViewModel.ProductName;
                foundedProduct.ProductOrigin = ProductViewModel.ProductOrigin;
                foundedProduct.ReceiptNumber = ProductViewModel.ReceiptNumber;
                foundedProduct.EntryDateTime = ProductViewModel.EntryDateTime;
                foundedProduct.ClearanceDateTime = ProductViewModel.ClearanceDateTime;
                foundedProduct.LeavingTheWarehouseDateTime = ProductViewModel.LeavingTheWarehouseDateTime;
                
                foundedProduct.SetUpdateDateTime();
                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.Product);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
                return Page();
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                AddPageError(message: Messages.NotFound);
                return Page();
            }
            else
            {
                AddToastError(message: Messages.UnexpectedError);
            }
        }

        return RedirectToPage("./Index");
    }

    private bool UserExists(int? id)
    {
        return DatabaseContext.Users.Any(e => e.ID == id);
    }
}
