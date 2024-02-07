namespace WebApp.ViewModels;

public class RegisterVM : User
{
    [Display(Name = nameof(DataDictionary.ConfirmPassword), ResourceType = typeof(DataDictionary))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [Compare(otherProperty: nameof(Password), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Compare))]
    [DataType(dataType: DataType.Password)]
    public string ConfirmPassword { get; set; }
}
