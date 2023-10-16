using OrderAggregate = CourseApp.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace CourseApp.Order.Infrastructure;

public class OrderDbContext : DbContext
{
        public const string DEFAULT_SCHEMA = "ordering";
    public DbSet<OrderAggregate.Order> Orders { get; set; }
    public DbSet<OrderAggregate.OrderItem> OrderItems { get; set; }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
        modelBuilder.Entity<OrderAggregate.Order>().OwnsOne(order => order.Address).WithOwner();
        
        modelBuilder.Entity<OrderAggregate.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
        modelBuilder.Entity<OrderAggregate.OrderItem>().Property(order => order.Price).HasColumnType("decimal(18,2)");

        base.OnModelCreating(modelBuilder);
    }

}
