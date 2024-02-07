namespace WebApp.Pages.Admin.Missions;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class EditModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var foundMissions = from f in DatabaseContext.Missions
                                where f.ID == id
                                select f;

            if (foundMissions.Any())
            {
                var mission = foundMissions.FirstOrDefault()!;
                mission.MissionName = MissionViewModel.MissionName;
                mission.MissionDescription = MissionViewModel.MissionDescription;
                mission.MissionDateTime = MissionViewModel.MissionDateTime;
                mission.SetUpdateDateTime();

                await DatabaseContext.SaveChangesAsync();

                var successMessage = string.Format(Messages.Updated, DataDictionary.Mission);
                AddToastSuccess(message: successMessage);
            }
            else
            {
                AddPageError(message: Messages.NotFound);
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MissionExists(id))
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

    private bool MissionExists(int? id)
    {
        return DatabaseContext.Missions.Any(e => e.ID == id);
    }
}
