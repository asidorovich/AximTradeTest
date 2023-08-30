using AximTradeTest.Models.Exceptions;
using AximTradeTest.Models.Models.TreeNode;
using AximTradeTest.Services.Repositories.Interfaces;
using AximTradeTest.Services.Services.Interfaces;
using Database;
using Microsoft.Extensions.Logging;

namespace AximTradeTest.Services.Services;

public class TreeNodeService : ITreeNodeService
{
    private readonly ITreeNodeReadRepository _treeNodeReadRepo;
    private readonly ITreeNodeWriteRepository _treeNodeWriteRepo;
    private readonly ILogger<ITreeNodeService> _logger;

    public TreeNodeService(ITreeNodeReadRepository treeNodeReadRepo,
        ITreeNodeWriteRepository treeNodeWriteRepo,
        ILogger<ITreeNodeService> logger)
    {
        _treeNodeReadRepo = treeNodeReadRepo;
        _treeNodeWriteRepo = treeNodeWriteRepo;
        _logger = logger;
    }

    public async Task<TreeNodeModel?> GetOrCreateTreeAsync(string name)
    {
        TreeNodeModel? result = null;

        var tree = await GetTreeAsync(name);

        if(tree == null)
        {
            result = await CreateNodeAsync(name, null);
        }

        return result;
    }

    public async Task<TreeNodeModel?> CreateTreeNodeAsync(CreateTreeNodeModel model)
    {
        TreeNodeModel? result = null;

        var treeNodes = await _treeNodeReadRepo.GetNodesByRootName(model.TreeName);

        var treeNode = FindTreeNode(treeNodes, model.ParentId);

        if (treeNode == null)
        {
            throw new SecurityException("Node does not exist in this tree");
        }

        var isSiblingNameUnique = CheckIfSiblingNameUnique(treeNodes, model.ParentId, model.TreeNodeName);

        if (!isSiblingNameUnique)
        {
            throw new SecurityException("Duplicate name");
        }
        else
        {
            result = await CreateNodeAsync(model.TreeNodeName, model.ParentId);
        }

        return result;
    }

    public async Task<bool> DeleteTreeNodeAsync(DeleteTreeNodeModel model)
    {
        var treeNodes = await _treeNodeReadRepo.GetNodesByRootName(model.TreeName);

        var treeNode = FindTreeNode(treeNodes, model.TreeNodeId);

        if (treeNode == null)
        {
            throw new SecurityException("Node does not exist in this tree", model);
        }

        var isTreeNodeHasChildren = CheckIfTreeNodeHasChildren(treeNodes, model.TreeNodeId);

        if (isTreeNodeHasChildren)
        {
            throw new SecurityException("You have to delete all children nodes first", model);
        }

        var result = _treeNodeWriteRepo.Delete(treeNode);
        return result;
    }

    public async Task<TreeNodeModel?> UpdateTreeNodeAsync(UpdateTreeNodeModel model)
    {
        TreeNodeModel? result = null;

        var treeNodes = await _treeNodeReadRepo.GetNodesByRootName(model.TreeName);

        var treeNode = FindTreeNode(treeNodes, model.TreeNodeId);

        if (treeNode == null)
        {
            throw new SecurityException("Node does not exist in this tree");
        }

        if (!treeNode.ParentId.HasValue)
        {
            throw new SecurityException("The tree name cannot be edited'");

        }
        var isSiblingNameUnique = CheckIfSiblingNameUnique(treeNodes, treeNode.ParentId.Value, model.NewTreeNodeName);

        if (!isSiblingNameUnique)
        {
            throw new SecurityException("Duplicate name");
        }
        else
        {
            treeNode.Name = model.NewTreeNodeName;
            var entity = _treeNodeWriteRepo.Update(treeNode);

            if (entity != null)
            {
                result = new TreeNodeModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }

        return result;
    }

    private async Task<TreeNodeModel?> CreateNodeAsync(string name, int? parentId)
    {
        TreeNodeModel? result = null;

        var treeNode = new TreeNode
        {
            Name = name,
            ParentId = parentId
        };

        var entity = await _treeNodeWriteRepo.InsertAsync(treeNode);

        if(entity != null)
        {
            result = new TreeNodeModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        return result;
    }

    private async Task<TreeNodeModel?> GetTreeAsync(string name)
    {
        TreeNodeModel? result = null;

        try
        {
            var treeNodes = await _treeNodeReadRepo.GetNodesByRootName(name);

            if (treeNodes?.Any() == true)
            {
                var rootNode = treeNodes.FirstOrDefault(x => x.ParentId is null);

                if (rootNode != null)
                {
                    result = new TreeNodeModel
                    {
                        Id = rootNode.Id,
                        Name = rootNode.Name,
                        Children = GetChildren(rootNode.Id, treeNodes)
                    };
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting a tree with name '{name}'");
        }

        return result;
    }

    private IEnumerable<TreeNodeModel> GetChildren(long parentId, IEnumerable<TreeNode> nodes)
    {
        var result = new List<TreeNodeModel>();

        var children = nodes.Where(x => x.ParentId == parentId);

        if(children?.Any() == true)
        {
            foreach(var child in children)
            {
                var treeNode = new TreeNodeModel
                {
                    Id = child.Id,
                    ParentId = parentId,
                    Name = child.Name,
                    Children = GetChildren(child.Id, nodes)
                };

                result.Add(treeNode);
            }
        }

        return result;
    }

    private static TreeNode? FindTreeNode(IEnumerable<TreeNode> treeNodes, int treeNodeId) =>
        treeNodes?.FirstOrDefault(x => x.Id == treeNodeId);

    private static bool CheckIfTreeNodeHasChildren(IEnumerable<TreeNode> treeNodes, int treeNodeId) =>
        treeNodes?.Any(x => x.ParentId == treeNodeId) == true;

    private static bool CheckIfSiblingNameUnique(IEnumerable<TreeNode> treeNodes, int parentTreeNodeId, string treeNodeName)
    {
        var nodeSiblings = treeNodes?.Where(x => x.ParentId == parentTreeNodeId);

        var isSiblingNameUnique = nodeSiblings?.Any(x => x.Name.ToLower() == treeNodeName.ToLower()) == false;

        return isSiblingNameUnique;
    }
}
