using Database.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Database;

public class AximTradeTestDbContext : DbContext
{
    public AximTradeTestDbContext(DbContextOptions<AximTradeTestDbContext> options)
            : base(options)
    {
    }

    public DbSet<TreeNode> TreeNodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TreeNodeConfiguration());
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
