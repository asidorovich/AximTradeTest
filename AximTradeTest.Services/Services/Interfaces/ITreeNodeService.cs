using AximTradeTest.Models.Models.TreeNode;

namespace AximTradeTest.Services.Services.Interfaces;

public interface ITreeNodeService
{
    Task<TreeNodeModel?> GetOrCreateTreeAsync(string name);

    Task<TreeNodeModel?> CreateTreeNodeAsync(CreateTreeNodeModel model);

    Task<TreeNodeModel?> UpdateTreeNodeAsync(UpdateTreeNodeModel model);

    Task<bool> DeleteTreeNodeAsync(DeleteTreeNodeModel model);
}
