using Microsoft.AspNetCore.Mvc;
using AximTradeTest.Models.Models;
using AximTradeTest.Services.Services.Interfaces;

namespace AximTradeTest.Controllers;

[ApiController]
[Produces("application/json")]
public class UserTreeController : Controller
{
    private readonly ITreeNodeService _treeNodeService;

    public UserTreeController(ITreeNodeService treeNodeService)
    {
        _treeNodeService = treeNodeService;
    }

    [HttpPost("/api.user.tree.get")]
    public async Task<TreeNodeModel?> GetTreeAsync([FromBody] string treeName)
    {
        var result = await _treeNodeService.GetOrCreateTreeAsync(treeName);

        return result;
    }
}
