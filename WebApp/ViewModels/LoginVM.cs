namespace WebApp.ViewModels;

public class LoginVM
{
    [Display(Name = nameof(DataDictionary.Username), ResourceType = typeof(DataDictionary))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [MaxLength(length: Constants.UsernameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [RegularExpression(pattern: @"^[a-zA-Z0-9_]{6,20}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Username))]
    public string Username { get; set; }


    [Display(Name = nameof(DataDictionary.Password), ResourceType = typeof(DataDictionary))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^[a-zA-Z0-9_]{8,20}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Password))]
    [DataType(dataType: DataType.Password)]
    public string Password { get; set; }


    [Display(Name = nameof(DataDictionary.RememberMe), ResourceType = typeof(DataDictionary))]
    public bool RememberMe { get; set; }
}
