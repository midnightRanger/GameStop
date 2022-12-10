using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStop.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

    public DbSet<UserModel> User { get; set; } = null!;
    public DbSet<AccountModel> Account { get; set; } = null!;
    public DbSet<AdminModel> Admin { get; set; } = null!;
    public DbSet<PlatformModel> Platform { get; set; } = null!; 
    public DbSet<ProductStatusModel> ProductStatus { get; set; } = null!;
    public DbSet<PaymentInfoModel> PaymentInfo { get; set; } = null!; 
    public DbSet<ProductInfoModel> ProductInfo { get; set; } = null!; 
    public DbSet<ProductModel> Product { get; set; } = null!; 
    public DbSet<EKeyModel> EKey { get; set; } = null!;
    public DbSet<LicenseModel> License { get; set; } = null!;
    public DbSet<CartModel> Cart { get; set; } = null!;
    public DbSet<OrderModel> Order { get; set; } = null!;
    public DbSet<ReviewModel> Review { get; set; } = null!;
    public ApplicationContext()
    {
    }
}