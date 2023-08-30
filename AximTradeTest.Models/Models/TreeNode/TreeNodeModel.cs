namespace AximTradeTest.Models.Models.TreeNode;

public class TreeNodeModel
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Name { get; set; }

    public IEnumerable<TreeNodeModel> Children { get; set; } = Enumerable.Empty<TreeNodeModel>();
}
