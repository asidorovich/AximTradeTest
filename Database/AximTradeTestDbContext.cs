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
        base.OnModelCreating(modelBuilder);
    }
}
