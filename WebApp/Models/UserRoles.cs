namespace WebApp.Models;

/// <summary>
/// نقش کاربران
/// </summary>
public class UserRoles
{
    public const string Administrator = "Administrator";
    
    public const string Accountant = "Accountant";
    
    public const string Warehouseman = "Warehouseman";
    
    public const string SalesExpert = "SalesExpert";
    
    public const string Collector = "Collector";

    public const string NewUser = "NewUser";


    public IList<string> GetRoles =
    [
        DataDictionary.Administrator,
        DataDictionary.Accountant,
        DataDictionary.Warehouseman,
        DataDictionary.SalesExpert,
        DataDictionary.Collector,
        DataDictionary.NewUser
    ];
}
