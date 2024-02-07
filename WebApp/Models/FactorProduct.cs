namespace WebApp.Models;

/// <summary>
/// این کاملا یک کلاس کمکی است و در دیتابیس ذخیره نمی شود
/// برای ثبت کالا در فاکتور کمک میکند
/// </summary>
public class FactorProduct
{
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ID))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ID { get; set; }

    /// <summary>
    /// شماره فاکتور
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FactorID))]
    public int FactorID { get; set; }

    /// <summary>
    /// شماره کالا
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductID))]
    public int ProductID { get; set; }

    /// <summary>
    /// قیمت واحد کالا
    /// </summary>
    /// 
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductPrice))]
	[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
	public int ProductPrice { get; set; }

	/// <summary>
	/// تعداد کالا
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductCount))]
	[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
	public int ProductCount { get; set; }

	/// <summary>
	/// مبلغ کل
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TotalAmount))]
	public int TotalAmount => ProductCount * ProductPrice;



    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Factor))]
    public Factor Factor { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Product))]
    public Product Product { get; set; }
}
