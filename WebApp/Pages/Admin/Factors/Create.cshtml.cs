namespace WebApp.Pages.Admin.Factors;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class CreateModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public FactorVM FactorViewModel { get; set; } = new();

    [BindProperty]
    public FactorProductVM FactorProductViewModel { get; set; } = new();

    public List<FactorProduct> FactorProductsList = [];

    private static readonly List<FactorProduct> _products = [];

    private static int _factorID = 0;
    private static int _fixFpID = 1;


    public void OnGet()
    {
        InitializeData();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            FactorViewModel.TotalAmount = _products.Sum(fp => fp.TotalAmount);

            var customer = await DatabaseContext.Customers.FindAsync(FactorViewModel.CustomerID);
            if (customer != null)
            {
                FactorViewModel.Customer = customer;
            }

            var entry = DatabaseContext.Add(new Factor()
            {
                ID = _factorID
            });

            entry.CurrentValues.SetValues(FactorViewModel);
            await DatabaseContext.SaveChangesAsync();

            foreach (var fp in _products)
            {
                var entry2 = DatabaseContext.Add(new FactorProduct());
                entry2.CurrentValues.SetValues(fp);
                await DatabaseContext.SaveChangesAsync();
            }

            _factorID = 0;
            _products.Clear();
            FactorProductsList.Clear();

            var successMessage = string.Format(Messages.Created, DataDictionary.Factor);
            AddToastSuccess(message: successMessage);
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }

        return RedirectToPage("./Index");
    }

    public IActionResult OnPostAddProduct()
    {
        try
        {
            var newFP = new FactorProduct
            {
                FactorID = _factorID,
                ProductID = FactorProductViewModel.ProductID,
                ProductPrice = FactorProductViewModel.ProductPrice,
                ProductCount = FactorProductViewModel.ProductCount
            };

            var fp = from p in DatabaseContext.FactorProducts
                     select p;

            if (_fixFpID == 1)
            {
                if (fp.Any())
                {
                    var maxId = fp.Max(p => p.ID);
                    _fixFpID = maxId + 1;
                }
            }

            newFP.ID = _fixFpID;
            _fixFpID++;

            _products.Add(newFP);
            FactorProductsList = _products;
            InitializeData();
            return Page();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
            return Page();
        }
    }

    private void InitializeData()
    {
        try
        {
            ViewData["CustomerID"] = new SelectList(DatabaseContext.Customers, "ID", "FullName");
            ViewData["ProductList"] = new SelectList(DatabaseContext.Products, "ID", "ProductName");

            var factors = from f in DatabaseContext.Factors
                          select f;

            if (factors.Any())
            {
                var all = factors.ToList();
                var maxId = all.Max(f => f.ID);

                if (_factorID == 0)
                {
                    _factorID = maxId + 1;
                }
            }
            else
            {
                _factorID = 1;
            }
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
