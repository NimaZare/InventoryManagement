namespace WebApp.Models;

/// <summary>
/// کلاس مربوط به کالا
/// </summary>
public class Product : Entity
{
    /// <summary>
    /// نوع کالا
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductCategory))]
    [MaxLength(length: Constants.ProductNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string Category { get; set; }

    /// <summary>
    /// نام کالا
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductName))]
    [MaxLength(length: Constants.ProductNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string ProductName { get; set; }

    /// <summary>
	/// تعداد کالا
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Count))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int Count { get; set; }

    /// <summary>
    /// مبدأ کالا
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductOrigin))]
    [MaxLength(length: Constants.ProductOriginMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string ProductOrigin { get; set; }

    /// <summary>
    /// شماره ی رسید کالا
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ReceiptNumber))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int ReceiptNumber { get; set; }

    /// <summary>
    /// تاریخ ورود به انبار
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.EntryDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime EntryDateTime { get; set; } = Utility.Now;

	/// <summary>
	/// تاریخ خروج کالا از انبار
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.LeavingTheWarehouseDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime LeavingTheWarehouseDateTime { get; set; } = Utility.Now;

	/// <summary>
	/// تاریخ ترخیص کالا
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ClearanceDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime ClearanceDateTime { get; set; } = Utility.Now;



	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FactorProducts))]
    public ICollection<FactorProduct> FactorProducts { get; set; }
}
