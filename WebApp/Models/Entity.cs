namespace WebApp.Models;

/// <summary>
/// این یک کلاس پایه برای فیلد های مشترک مدل هاست،
/// تمام فیلد هایی که در همه مدل ها مشترک است در اینجا نوشته می شود
/// </summary>
public abstract class Entity : IEntity<int>
{
    /// <summary>
    /// آیدی یکتا مربوط به دیتابیس می باشد،
    /// علت نوشتن آیدی یکتا این است تا وقتی بخواهیم دیتاها را تجمیع کنیم،
    /// داده های یکسان یا آیدی های مشابه نداشته باشیم
    /// </summary>
    [Key]
    [Display(ResourceType = typeof(DataDictionary), Name = nameof(DataDictionary.ID))]
    public int ID { get; private set; }


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

    /// <summary>
    /// ثبت آیدی دیفالت بصورت دستی
    /// </summary>
    /// <param name="id">آیدی جدید</param>
    public void SetID(int id)
    {
        ID = id;
    }
}
