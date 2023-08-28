namespace AximTradeTest.Models.Models;

public class TreeNodeModel
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; }

    public IEnumerable<TreeNodeModel> Children { get; set; } = Enumerable.Empty<TreeNodeModel>();
}
