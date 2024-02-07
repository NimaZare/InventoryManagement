namespace WebApp.Models;

public class ProductCategories
{
    public const string Vitamins = "Vitamins";

    public const string Minerals = "Minerals";


    public IList<string> GetCategories =
    [
        DataDictionary.Vitamins,
        DataDictionary.Minerals
    ];
}
