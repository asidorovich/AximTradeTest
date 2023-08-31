using Dapper;
using Database;
using Microsoft.Extensions.Configuration;
using AximTradeTest.Services.Repositories.Interfaces;
using MySqlConnector;

namespace AximTradeTest.Services.Repositories;

public class TreeNodeReadRepository : ReadRepository, ITreeNodeReadRepository
{
    public TreeNodeReadRepository(IConfiguration configuration)
        : base(configuration)
    {
    }

    public async Task<IEnumerable<TreeNode>> GetNodesByRootName(string name)
    {
        IEnumerable<TreeNode> result = Enumerable.Empty<TreeNode>();

        using var connection = new MySqlConnection(ConnectionString);
        var sqlQuery = @"
WITH RECURSIVE cte_node (id, `name`, parent_id)
AS
(
	SELECT id, 
        `name`, 
        parent_id
	FROM tree_node
	WHERE parent_id IS NULL
		AND `name` = @treeName
	
	UNION ALL
	
	SELECT tn.id, 
        tn.name, 
        tn.parent_id
	FROM cte_node
	INNER JOIN tree_node tn ON cte_node.id = tn.parent_id
)

SELECT 
    id,
    `name`,
    parent_id as ParentId
FROM cte_node 
ORDER BY parent_id;
            ";

        var treeNodes = await connection.QueryAsync<TreeNode>(sqlQuery, new { treeName = name });

        if (treeNodes?.Any() == true)
        {
            result = treeNodes;
        }

        return result;
    }

    public async Task<IEnumerable<TreeNode>> GetNodesByParentId(string treeName, int? parentId = null)
    {
        IEnumerable<TreeNode> result = Enumerable.Empty<TreeNode>();

        using var connection = new MySqlConnection(ConnectionString);
        var sqlQuery = @"
SELECT id, 
    `name`, 
    parent_id
FROM tree_node
WHERE (@parentId IS NULL AND parent_id IS NULL) 
    OR (@parentId IS NOT NULL AND parent_id = @parentId) ";

        var treeNodes = await connection.QueryAsync<TreeNode>(sqlQuery, new { parentId });

        if (treeNodes?.Any() == true)
        {
            result = treeNodes;
        }

        return result;
    }
}
