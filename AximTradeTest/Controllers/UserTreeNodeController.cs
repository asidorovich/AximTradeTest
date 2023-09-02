using AximTradeTest.Models.Models.TreeNode;
using AximTradeTest.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Controllers;

[Tags("user.tree.node")]
//[SwaggerTag("Represents tree node API")]
[ApiController]
[Produces("application/json")]
public class UserTreeNodeController : ControllerBase
{
    private readonly ITreeNodeService _treeNodeService;

    public UserTreeNodeController(ITreeNodeService treeNodeService) 
        => (_treeNodeService) = (treeNodeService);

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Create a new node in your tree. You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.
    /// </remarks>
    /// <param name="treeName"></param>
    /// <param name="treeNodeName"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    [HttpPost("/api.user.tree.create")]
    public async Task<IActionResult> CreateUserTreeNodeAsync([Required] string treeName, [Required] string treeNodeName, [Required] int parentId)
    {
        var model = new CreateTreeNodeModel
        {
            TreeName = treeName,
            TreeNodeName = treeNodeName,
            ParentId = parentId
        };

        await _treeNodeService.CreateTreeNodeAsync(model);
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Delete an existing node in your tree. You must specify a node ID that belongs your tree.
    /// </remarks>
    /// <param name="treeName"></param>
    /// <param name="treeNodeId"></param>
    /// <returns></returns>
    [HttpPost("/api.user.tree.delete")]
    public async Task<IActionResult> DeleteUserTreeNodeAsync([Required] string treeName, [Required] int treeNodeId)
    {
        var model = new DeleteTreeNodeModel
        {
            TreeName = treeName,
            TreeNodeId = treeNodeId
        };

        await _treeNodeService.DeleteTreeNodeAsync(model);
        return Ok();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Rename an existing node in your tree. You must specify a node ID that belongs your tree. A new name of the node must be unique across all siblings.
    /// </remarks>
    /// <param name="treeName"></param>
    /// <param name="treeNodeId"></param>
    /// <param name="newTreeNodeName"></param>
    /// <returns></returns>
    [HttpPost("/api.user.tree.rename")]
    public async Task<IActionResult> RenameUserTreeNodeAsync([Required] string treeName, [Required] int treeNodeId, [Required] string newTreeNodeName)
    {
        var model = new UpdateTreeNodeModel
        {
            TreeName = treeName,
            TreeNodeId = treeNodeId,
            NewTreeNodeName = newTreeNodeName
        };

        await _treeNodeService.UpdateTreeNodeAsync(model);
        return Ok();
    }
}
