namespace WebApp.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options: options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Fund> Funds { get; set; }
    public DbSet<Factor> Factors { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<FactorProduct> FactorProducts { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
