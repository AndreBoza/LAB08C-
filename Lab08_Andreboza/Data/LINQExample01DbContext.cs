using Lab08_Andreboza.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab08_Andreboza.Data;

// 1. La clase ahora hereda de IdentityDbContext<IdentityUser>
public class LINQExample01DbContext : IdentityDbContext<IdentityUser>
{
    // Solo necesitamos el constructor que recibe 'options'
    public LINQExample01DbContext(DbContextOptions<LINQExample01DbContext> options)
        : base(options)
    {
    }

    // Tus DbSets se mantienen igual
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Orderdetail> Orderdetails { get; set; }
    public virtual DbSet<Product> Products { get; set; }

    // 2. El método OnConfiguring se elimina porque la conexión ya se configura en Program.cs

    // El método OnModelCreating se mantiene para tus tablas existentes
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // MUY IMPORTANTE: Llama a la implementación base primero
        base.OnModelCreating(modelBuilder); 

        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");
            entity.ToTable("clients");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");
            entity.ToTable("orders");
            entity.HasIndex(e => e.ClientId, "ClientId");
            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.OrderDate).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("current_timestamp()").HasColumnType("timestamp");
            entity.HasOne(d => d.Client).WithMany(p => p.Orders).HasForeignKey(d => d.ClientId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("orders_ibfk_1");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");
            entity.ToTable("orderdetails");
            entity.HasIndex(e => e.OrderId, "OrderId");
            entity.HasIndex(e => e.ProductId, "ProductId");
            entity.Property(e => e.OrderDetailId).HasColumnType("int(11)");
            entity.Property(e => e.OrderId).HasColumnType("int(11)");
            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails).HasForeignKey(d => d.OrderId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("orderdetails_ibfk_1");
            entity.HasOne(d => d.Product).WithMany(p => p.Orderdetails).HasForeignKey(d => d.ProductId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("orderdetails_ibfk_2");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");
            entity.ToTable("products");
            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasPrecision(10, 2);
        });
    }
}