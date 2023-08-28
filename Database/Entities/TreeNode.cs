using System;
using System.Collections.Generic;
using System.Text;

namespace Database;

public class TreeNode : Entity
{
    public string Name { get; set; }

    public int? ParentId { get; set; }
}
