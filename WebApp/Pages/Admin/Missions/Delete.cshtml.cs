namespace WebApp.Pages.Admin.Missions;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class DeleteModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    [BindProperty]
    public Mission MissionViewModel { get; set; } = default!;


    public IActionResult OnGet(int? id)
    {
        try
        {
            if (id != null)
            {
                var foundMissions = from f in DatabaseContext.Missions
                                    where f.ID == id
                                    select f;

                if (foundMissions.Any())
                {
                    MissionViewModel = foundMissions.FirstOrDefault()!;
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
                var foundMissions = from f in DatabaseContext.Missions
                                    where f.ID == id
                                    select f;

                if (foundMissions.Any())
                {
                    MissionViewModel = foundMissions.FirstOrDefault()!;
                    DatabaseContext.Missions.Remove(MissionViewModel);
                    await DatabaseContext.SaveChangesAsync();

                    var successMessage = string.Format(Messages.Deleted, DataDictionary.Mission);
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
