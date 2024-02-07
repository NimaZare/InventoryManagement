namespace WebApp.ViewModels;

public class FactorVM
{
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FactorID))]
    public int FactorID { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.CustomerID))]
    public int CustomerID { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Paid))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int Paid { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TotalAmount))]
    public int TotalAmount { get; set; }


    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Customer))]
    public Customer Customer { get; set; }
}
