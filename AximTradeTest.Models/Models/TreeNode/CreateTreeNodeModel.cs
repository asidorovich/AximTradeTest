using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.TreeNode;

public class CreateTreeNodeModel
{
    [Required]
    [BindProperty(Name = "treeName")]
    public string TreeName { get; set; }

    [Required]
    [BindProperty(Name = "treeNodeName")]
    public string TreeNodeName { get; set; }

    [Required]
    [BindProperty(Name = "parentId")]
    public int ParentId { get; set; }
}
