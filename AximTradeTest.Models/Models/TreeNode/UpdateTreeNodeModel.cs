using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.TreeNode;

public class UpdateTreeNodeModel
{
    [Required]
    [BindProperty(Name = "treeName")]
    public string TreeName { get; set; }

    [Required]
    [BindProperty(Name = "treeNodeId")]
    public int TreeNodeId { get; set; }

    [Required]
    [BindProperty(Name = "newTreeNodeName")]
    public string NewTreeNodeName { get; set; }
}
