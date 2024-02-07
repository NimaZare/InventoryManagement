namespace WebApp.Pages.Admin.Missions;

[Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Collector}")]
public class IndexModel(StoreContext context) : BasePageModelWithDatabaseContext(context)
{
    public IList<Mission> MissionViewModel { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? SearchDateTime { get; set; }


    public async Task OnGetAsync()
    {
        try
        {
            var missions = from c in DatabaseContext.Missions
                           orderby c.UpdateDateTime descending
                           select c;

            if (!string.IsNullOrEmpty(SearchString))
            {
                missions = missions.Where(c => c.MissionName.Contains(SearchString))
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            if (SearchDateTime != null)
            {
                missions = missions.Where(c => c.MissionDateTime.Date == SearchDateTime)
                    .OrderByDescending(c => c.UpdateDateTime);
            }

            MissionViewModel = await missions.ToListAsync();
        }
        catch
        {
            AddPageError(message: Messages.UnexpectedError);
        }
    }
}
