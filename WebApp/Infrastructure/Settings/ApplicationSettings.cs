namespace Infrastructure.Settings;

public class ApplicationSettings
{
	public static readonly string KeyName = nameof(ApplicationSettings);

	public ApplicationSettings()
	{
		CultureSettings = new CultureSettings();
		ToastSettings = new ToastSettings();
	}


	public string Version { get; set; }

	public CultureSettings CultureSettings { get; set; }

    public ToastSettings ToastSettings { get; set; }

}
