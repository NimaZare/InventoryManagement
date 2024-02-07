namespace WebApp.Models;

/// <summary>
/// این کلاس مشتری می باشد
/// </summary>
public class Customer : Entity
{
    /// <summary>
    /// نام مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FirstName))]
    [MaxLength(length: Constants.FirstNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string FirstName { get; set; }

    /// <summary>
    /// نام خانوادگی مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.LastName))]
    [MaxLength(length: Constants.LastNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string LastName { get; set; }

    /// <summary>
    /// این فیلد در دیتابیس ذخیره نمی شود و صرفاً کمکی می باشد،
    /// کاربرد آن نشان دادن نام کامل مشتریست
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FullName))]
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// کدملی مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.NationalCode))]
    [MaxLength(length: Constants.NationalCode, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^\d{10}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NationalCode))]
    public string NationalCode { get; set; }

    /// <summary>
    /// شماره تلفن همراه مربوط به مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TellNumber))]
    [MaxLength(length: Constants.TellNumberMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^09\d{9}", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.TellNumberNotValid))]
    [DataType(DataType.PhoneNumber)]
    public string TellNumber { get; set; }

    /// <summary>
    /// آدرس محل سکونت مشتری
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Address))]
    [MaxLength(length: Constants.AddressMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string Address { get; set; }



    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Factors))]
    public ICollection<Factor> Factors { get; set; }

    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Invoices))]
    public ICollection<Invoice> Invoices { get; set; }

	//  [DisplayFormat(NullDisplayText = "It's Null")]
}
