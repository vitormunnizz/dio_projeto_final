using Microsoft.EntityFrameworkCore;
using EcommerceSimples.Models;

namespace EcommerceSimples.Data;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> opts) : base(opts) {}

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
}