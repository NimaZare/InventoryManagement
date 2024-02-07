using Infrastructure.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddRazorPages();

builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>
    (builder.Configuration.GetSection(key: Infrastructure.Settings.ApplicationSettings.KeyName))
    .AddSingleton(implementationFactory: serviceType =>
    {
        var result = serviceType.GetRequiredService
            <Microsoft.Extensions.Options.IOptions
            <Infrastructure.Settings.ApplicationSettings>>().Value;

        return result;
    });

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddAuthentication(defaultScheme: Constants.AuthenticationScheme)
    .AddCookie(authenticationScheme: Constants.AuthenticationScheme)
    .AddGoogle(authenticationScheme: Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme,
    configureOptions: options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
        options.ClaimActions.MapJsonKey(claimType: "urn:google:picture", jsonKey: "picture", valueType: "url");
    });

var connectionString = builder.Configuration.GetConnectionString(name: "StoreCon");

builder.Services.AddDbContext<StoreContext>(optionsAction: options =>
{
    options.UseSqlServer(connectionString: connectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<StoreContext>();
	context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCultureCookie();
app.MapRazorPages();

app.Run();
