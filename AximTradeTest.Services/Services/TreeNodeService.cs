using AximTradeTest.Models.Exceptions;
using AximTradeTest.Models.Models;
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

        var isParentIdInTree = treeNodes?.Any(x => x.Id == model.ParentId) == true;

        if (!isParentIdInTree)
        {
            throw new SecurityException($"The provided Parent Id ('{model.ParentId}') does not belong to a tree with name '{model.TreeName}'");
        }

        var nodeSiblings = treeNodes?.Where(x => x.ParentId == model.ParentId);

        var isSiblingNameUnique = nodeSiblings?.Any(x => x.Name.ToLower() == model.TreeNodeName.ToLower()) == false;

        if (!isSiblingNameUnique)
        {
            throw new SecurityException($"The provided node name ('{model.TreeNodeName}') is not unique for siblings of parent with id '{model.ParentId}'");
        }
        else
        {
            result = await CreateNodeAsync(model.TreeNodeName, model.ParentId);
        }

        return result;
    }

    public async Task<bool> DeleteTreeNodeAsync(string treeName, int treeNodeId)
    {
        var result = false;

        var treeNodes = await _treeNodeReadRepo.GetNodesByRootName(treeName);

        var treeNode = treeNodes?.FirstOrDefault(x => x.Id == treeNodeId);

        if (treeNode == null)
        {
            throw new SecurityException($"The provided Tree Node Id ('{treeNodeId}') does not belong to a tree with name '{treeName}'");
        }

        result = _treeNodeWriteRepo.Delete(treeNode);

        return result;
    }

    private async Task<TreeNodeModel?> CreateNodeAsync(string name, int? parentId)
    {
        TreeNodeModel? result = null;

        try
        {
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
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error creating a tree with name '{name}'");
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

    private IEnumerable<TreeNodeModel> GetChildren(int parentId, IEnumerable<TreeNode> nodes)
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
}
