namespace WebApp.Pages.Admin.Missions;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class CreateModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Mission MissionViewModel { get; set; } = default!;


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

            DatabaseContext.Missions.Add(MissionViewModel);
            await DatabaseContext.SaveChangesAsync();

            var successMessage = string.Format(format: Messages.Created, arg0: DataDictionary.Mission);
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
