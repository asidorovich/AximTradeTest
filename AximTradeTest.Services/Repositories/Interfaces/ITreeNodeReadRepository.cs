using Database;

namespace AximTradeTest.Services.Repositories.Interfaces;

public interface ITreeNodeReadRepository
{
    Task<IEnumerable<TreeNode>> GetNodesByRootName(string name);

    Task<IEnumerable<TreeNode>> GetNodesByParentId(string treeName, int? parentId = null);
}
