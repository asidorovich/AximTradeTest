using Database;

namespace AximTradeTest.Services.Repositories.Interfaces;

public interface ITreeNodeWriteRepository
{
    Task<TreeNode?> InsertAsync(TreeNode treeNode);

    TreeNode? Update(TreeNode treeNode);

    bool Delete(TreeNode treeNode);
}
