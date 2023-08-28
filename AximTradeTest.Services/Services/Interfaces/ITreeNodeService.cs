using AximTradeTest.Models.Models;

namespace AximTradeTest.Services.Services.Interfaces;

public interface ITreeNodeService
{
    Task<TreeNodeModel?> GetOrCreateTreeAsync(string name);

    Task<TreeNodeModel?> CreateTreeNodeAsync(CreateTreeNodeModel model);

    Task<bool> DeleteTreeNodeAsync(string treeName, int treeNodeId);
}
