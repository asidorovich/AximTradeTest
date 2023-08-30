using AximTradeTest.Models.Models.TreeNode;
using AximTradeTest.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Controllers;

public class UserTreeNodeController : Controller
{
    private readonly ITreeNodeService _treeNodeService;

    public UserTreeNodeController(ITreeNodeService treeNodeService) 
        => (_treeNodeService) = (treeNodeService);

    [HttpPost("/api.user.tree.create")]
    public async Task<bool> CreateUserTreeNodeAsync(CreateTreeNodeModel model)
    {
        var treeNode = await _treeNodeService.CreateTreeNodeAsync(model);
        
        //if(treeNode == null)
        //{
        //    return 
        //}
        return true;
    }

    [HttpPost("/api.user.tree.delete")]
    public async Task<bool> DeleteUserTreeNodeAsync(DeleteTreeNodeModel model)
    {
        var result = await _treeNodeService.DeleteTreeNodeAsync(model);

        return result;
    }

    [HttpPost("/api.user.tree.rename")]
    public async Task<bool> RenameUserTreeNodeAsync(UpdateTreeNodeModel model)
    {
        var result = await _treeNodeService.UpdateTreeNodeAsync(model);
        return true;
    }
}
