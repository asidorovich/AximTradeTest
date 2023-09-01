using Microsoft.AspNetCore.Mvc;
using AximTradeTest.Models.Models.TreeNode;
using AximTradeTest.Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace AximTradeTest.Controllers;

[Tags("user.tree")]
[ApiController]
[Produces("application/json")]
public class UserTreeController : ControllerBase
{
    private readonly ITreeNodeService _treeNodeService;

    public UserTreeController(ITreeNodeService treeNodeService)
        => (_treeNodeService) = (treeNodeService);

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Returns your entire tree. If your tree doesn't exist it will be created automatically.
    /// </remarks>
    /// <param name="treeName"></param>
    /// <returns></returns>
    [HttpPost("/api.user.tree.get")]
    public async Task<TreeNodeModel?> GetTreeAsync([Required] string treeName)
    {
        var result = await _treeNodeService.GetOrCreateTreeAsync(treeName);

        return result;
    }
}
