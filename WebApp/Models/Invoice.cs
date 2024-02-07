namespace WebApp.Models;

/// <summary>
/// این کلاس صورتحساب می باشد
/// </summary>
public class Invoice : Entity
{
    /// <summary>
    /// شماره مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.CustomerID))]
    public int CustomerID { get; set; }

    /// <summary>
	/// مبلغ کل فاکتورها
	/// </summary>
	[Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TotalAmount))]
    public int FactorsTotalAmount { get; set; }

    /// <summary>
    /// مبلغ کل پرداخت شده توسط مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Paid))]
	[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
	public int Paid { get; set; }

    /// <summary>
    /// مانده بدهی
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Debt))]
    public int Debt { get; private set; }

    /// <summary>
    /// محاسبه مقدار بدهی
    /// </summary>
    public void CalculateInvoiceDebt()
    {
        Debt = FactorsTotalAmount - Paid;
    }



    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Customer))]
    public Customer Customer { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Factors))]
    public ICollection<Factor> Factors { get; set; }
}
