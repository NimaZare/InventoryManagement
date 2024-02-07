namespace WebApp.Pages.Admin.Missions;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class DetailsModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
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
}
