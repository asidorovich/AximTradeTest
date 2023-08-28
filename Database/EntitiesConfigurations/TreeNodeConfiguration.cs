using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database;

public class TreeNodeConfiguration : IEntityTypeConfiguration<TreeNode>
{
    public void Configure(EntityTypeBuilder<TreeNode> builder)
    {
        builder.ToTable("tree_node");

        builder.HasKey(x => x.Id)
            .HasName("PK_tree_node");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.Property(x => x.ParentId)
            .HasColumnName("parent_id");
    }
}
