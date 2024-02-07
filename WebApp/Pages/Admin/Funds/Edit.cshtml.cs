namespace WebApp.Pages.Admin.Funds;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Fund FundViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundFund = from f in DatabaseContext.Funds
                                where f.ID == id
                                select f;

                if (foundFund.Any())
                {
                    FundViewModel = foundFund.FirstOrDefault()!;
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foundFund = from f in DatabaseContext.Funds
                            where f.ID == id
                            select f;

            if (foundFund.Any())
            {
                var fund = foundFund.FirstOrDefault()!;
                fund.ChargeDateTime = FundViewModel.ChargeDateTime;
                fund.BalanceCharge = FundViewModel.BalanceCharge;
                fund.ChargeUsed = FundViewModel.ChargeUsed;
                fund.FundDescription = FundViewModel.FundDescription;
                fund.SetUpdateDateTime();

                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.Fund);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FundExists(id))
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

    private bool FundExists(int? id)
    {
        return DatabaseContext.Funds.Any(e => e.ID == id);
    }
}
