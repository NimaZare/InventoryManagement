namespace WebApp.Models;

/// <summary>
/// کلاس مربوط به فاکتور مشتری
/// </summary>
public class Factor
{
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ID))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ID { get; set; }

    /// <summary>
    /// شماره مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.CustomerID))]
    public int CustomerID { get; set; }

	/// <summary>
	/// مبلغ کل فاکتور
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TotalAmount))]
	public int TotalAmount { get; set; }

    /// <summary>
    /// زمان ویرایش
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.UpdateDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public DateTime UpdateDateTime { get; private set; } = Utility.Now;

	/// <summary>
	/// تاریخ ثبت در دیتابیس
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.InsertDateTime))]
    public DateTime InsertDateTime { get; private set; } = Utility.Now;


	/// <summary>
	/// بروزرسانی زمان ویرایش
	/// </summary>
	public void SetUpdateDateTime()
    {
        UpdateDateTime = Utility.Now;
	}



    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Customer))]
    public Customer Customer { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FactorProducts))]
    public ICollection<FactorProduct> FactorProducts { get; set; }
}
