using AximTradeTest.Services.Repositories.Interfaces;
using Database;

namespace AximTradeTest.Services.Repositories;

public class TreeNodeWriteRepository : ITreeNodeWriteRepository
{
    private readonly AximTradeTestDbContext _dbContext;

    public TreeNodeWriteRepository(AximTradeTestDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TreeNode?> InsertAsync(TreeNode treeNode)
    {
        var entity = await _dbContext.AddAsync(treeNode);

        _dbContext.SaveChanges();

        return entity.Entity;
    }

    public async Task<TreeNode?> UpdateAsync(TreeNode treeNode)
    {
        var entity = await _dbContext.AddAsync(treeNode);

        _dbContext.SaveChanges();

        return entity.Entity;
    }

    public bool Delete(TreeNode treeNode)
    {
        _dbContext.Remove(treeNode);

        var result = _dbContext.SaveChanges();

        return result > 0;
    }
}
