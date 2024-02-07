namespace WebApp.Models;

/// <summary>
/// کلاس مربوط به تنخواه تحصیلدار
/// </summary>
public class Fund : Entity
{
    /// <summary>
    /// تاریخ شارژ
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ChargeDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime ChargeDateTime { get; set; } = Utility.Now;

	/// <summary>
	/// مبلغ شارژ
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.BalanceCharge))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int BalanceCharge { get; set; }

    /// <summary>
    /// مبلغ هزینه شده
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ChargeUsed))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int ChargeUsed { get; set; }

    /// <summary>
    /// شرح هزینه انجام شده
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FundDescription))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [DataType(DataType.MultilineText)]
    public string FundDescription { get; set; }
}
