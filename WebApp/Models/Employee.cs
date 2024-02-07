namespace WebApp.Models;

/// <summary>
/// این کلاس مربوط به پرسنل می باشد
/// </summary>
public class Employee : Entity
{
    /// <summary>
    /// کد پرسنلی
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.PersonnelCode))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int PersonnelCode { get; set; }

    /// <summary>
    /// نام پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FirstName))]
    [MaxLength(length: Constants.FirstNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string FirstName { get; set; }

    /// <summary>
    /// نام خانوادگی پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.LastName))]
    [MaxLength(length: Constants.LastNameMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public string LastName { get; set; }

    /// <summary>
    /// این فیلد در دیتابیس ذخیره نمی شود و صرفاً کمکی می باشد،
    /// کاربرد آن نشان دادن نام کامل پرسنل است
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.FullName))]
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// تاریخ تولد پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.BirthDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime BirthDateTime { get; set; } = Utility.Now;

    /// <summary>
    /// کدملی پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.NationalCode))]
    [MaxLength(length: Constants.NationalCode, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^\d{10}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.NationalCode))]
    public string NationalCode { get; set; }

    /// <summary>
    /// شماره تلفن همراه مربوط به پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.TellNumber))]
    [MaxLength(length: Constants.TellNumberMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [RegularExpression(pattern: @"^09\d{9}", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.TellNumberNotValid))]
    [DataType(DataType.PhoneNumber)]
    public string TellNumber { get; set; }

    /// <summary>
    /// آدرس محل سکونت پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.Address))]
    [MaxLength(length: Constants.AddressMax, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.MaxLength))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    [DataType(DataType.MultilineText)]
    public string Address { get; set; }

    /// <summary>
    /// کد پستی پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.PostalCode))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int PostCode { get; set; }

    /// <summary>
    /// تاریخ عضویت پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.PersonnelEntryDateTime))]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataType(DataType.Date)]
    public DateTime PersonnelEntryDateTime { get; set; } = Utility.Now;

    /// <summary>
    /// کد بیمه پرسنل
    /// </summary>
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.InsuranceCode))]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = nameof(Messages.Required))]
    public int InsuranceCode { get; set; }
}
