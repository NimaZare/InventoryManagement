namespace Infrastructure;

public abstract class BasePageModelWithDatabaseContext(StoreContext databaseContext) : BasePageModel
{
    protected StoreContext DatabaseContext => databaseContext;

    protected async Task DisposeDatabaseContextAsync()
	{
		if (DatabaseContext != null)
		{
			await DatabaseContext.DisposeAsync();
		}
	}
}
