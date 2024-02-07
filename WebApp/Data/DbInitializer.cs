namespace WebApp.Data;

public static class DbInitializer
{
	private static readonly PasswordHasher _passwordHasher = new();

	public static void Initialize(StoreContext context)
	{
        if (context.Users.Any()) return;

        var userAdmin = new User
		{
			FirstName = "nima",
			LastName = "zare",
			Username = "mrnima",
			TellNumber = "09112345678",
			Role = UserRoles.Administrator,
			EmailAddress = "info@nimazare.org",
			Password = _passwordHasher.Hash("Nima2024")
		};

		var newCustomer1 = new Customer
		{
			FirstName = "نیما",
			LastName = "زارع",
			NationalCode = "2133533535",
			TellNumber = "09112222222",
			Address = "آمل"
		};

        var newCustomer2 = new Customer
        {
            FirstName = "رویا",
            LastName = "نجفی",
            NationalCode = "2130454545",
            TellNumber = "09125555555",
            Address = "تهران"
        };

		var newProduct1 = new Product
		{
			Category = ProductCategories.Minerals,
			ProductName = "برنج",
			Count = 1000,
			ProductOrigin = "آمل",
			ReceiptNumber = 10089,
			EntryDateTime = Utility.Now,
			LeavingTheWarehouseDateTime = Utility.Now,
			ClearanceDateTime = Utility.Now
		};

        var newProduct2 = new Product
        {
			Category = ProductCategories.Vitamins,
            ProductName = "قهوه",
			Count = 100,
            ProductOrigin = "تهران",
            ReceiptNumber = 10050,
            EntryDateTime = Utility.Now,
            LeavingTheWarehouseDateTime = Utility.Now,
            ClearanceDateTime = Utility.Now
    };

        context.Users.Add(userAdmin);
		context.Customers.Add(newCustomer1);
		context.Customers.Add(newCustomer2);
		context.Products.Add(newProduct1);
		context.Products.Add(newProduct2);

        context.SaveChanges();
	}
}