namespace WebApp.ViewModels;

public class FactorProductVM
{
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FactorID))]
    public int FactorID { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductID))]
    public int ProductID { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductPrice))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int ProductPrice { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ProductCount))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int ProductCount { get; set; }
}
