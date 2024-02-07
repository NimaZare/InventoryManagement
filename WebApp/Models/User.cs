namespace WebApp.Models;

/// <summary>
/// این کلاس مربوط به کاربر می باشد
/// </summary>
public class User : Entity
{
    /// <summary>
    /// نام کاربر
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FirstName))]
    [MaxLength(length: Constants.FirstNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string FirstName { get; set; }

    /// <summary>
    /// نام خانوادگی کاربر
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.LastName))]
    [MaxLength(length: Constants.LastNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string LastName { get; set; }

    /// <summary>
    /// این فیلد در دیتابیس ذخیره نمی شود و صرفاً کمکی می باشد،
    /// کاربرد آن نشان دادن نام کامل است
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FullName))]
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// نقش کاربر
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Role))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string Role { get; set; }

    /// <summary>
    /// شناسه کاربری برای ورود
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Username))]
    [MaxLength(length: Constants.UsernameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string Username { get; set; }

    /// <summary>
    /// رمز ورود کاربر
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Password))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// آدرس ایمیل کاربر
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.EmailAddress))]
    [MaxLength(length: Constants.EmailAddressMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9_.-]+$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.EmailAddressNotValid))]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }

    /// <summary>
    /// شماره همراه کاربر
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TellNumber))]
    [MaxLength(length: Constants.TellNumberMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^09\d{9}", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.TellNumberNotValid))]
    [DataType(DataType.PhoneNumber)]
    public string TellNumber { get; set; }
}
