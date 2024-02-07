namespace WebApp.Models;

/// <summary>
/// کلاس مربوط به ماموریت تحصیلدار
/// </summary>
public class Mission : Entity
{
    /// <summary>
    /// عنوان مأموریت
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.MissionName))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string MissionName { get; set; }

    /// <summary>
    /// تاریخ مأموریت
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.MissionDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime MissionDateTime { get; set; } = Utility.Now;

	/// <summary>
	/// شرح مأموریت
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.MissionDescription))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [DataType(DataType.MultilineText)]
    public string MissionDescription { get; set; }
}
