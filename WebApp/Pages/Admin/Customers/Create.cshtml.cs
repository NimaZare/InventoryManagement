namespace WebApp.Pages.Admin.Customers;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.SalesExpert}")]
public class CreateModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Customer CustomerViewModel { get; set; } = default!;


	public IActionResult OnGet()
	{
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

            var foundCustomer = from c in DatabaseContext.Customers
								where c.NationalCode == CustomerViewModel.NationalCode
                                select c;


            if (!foundCustomer.Any())
			{
				CustomerViewModel.SetUpdateDateTime();
                DatabaseContext.Customers.Add(CustomerViewModel);
                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.Customer);
                AddToastSuccess(message: successMessage);
			}
            else
            {
                var errorMessage = string.Format(format: Messages.AlreadyExists, arg0: DataDictionary.Customer);
                AddPageWarning(message: errorMessage);
                return OnGet();
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
