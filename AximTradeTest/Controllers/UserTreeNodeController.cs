using AximTradeTest.Models.Models;
using AximTradeTest.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AximTradeTest.Controllers;

public class UserTreeNodeController : Controller
{
    private readonly ITreeNodeService _treeNodeService;

    public UserTreeNodeController(ITreeNodeService treeNodeService)
    {
        _treeNodeService = treeNodeService;
    }

    [HttpPost("/api.user.tree.create")]
    public async Task<bool> CreateUserTreeNodeAsync([FromBody] CreateTreeNodeModel model)
    {
        var treeNode = await _treeNodeService.CreateTreeNodeAsync(model);
        
        //if(treeNode == null)
        //{
        //    return 
        //}
        return true;
    }

    [HttpPost("/api.user.tree.delete")]
    public async Task<bool> DeleteUserTreeNodeAsync([FromBody] string treeName, int treeNodeId)
    {
        var result = await _treeNodeService.DeleteTreeNodeAsync(treeName, treeNodeId);

        return result;
    }

    [HttpPost("/api.user.tree.rename")]
    public async Task<bool> RenameUserTreeNodeAsync([FromBody] string treeName)
    {
        return true;
    }
}
