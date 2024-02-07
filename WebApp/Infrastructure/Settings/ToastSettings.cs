namespace Infrastructure.Settings;

public class ToastSettings
{
	public ToastSettings()
	{
		DelayStep = 1000;
		InitialDelay = 5000;
        Style = "top-25 end-0 p-3 opacity-50";
    }

    public string Style { get; set; }

    public int DelayStep { get; set; }

    public int InitialDelay { get; set; }

}
