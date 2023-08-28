//using AximTradeTest.Services.Repositories.Interfaces;
//using Database;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AximTradeTest.Services.Repositories;

//public class TreeNodeRepository : ITreeNodeRepository
//{
//    private readonly AximTradeTestDbContext _dbContext;

//    public TreeNodeRepository(AximTradeTestDbContext dbContext)
//    {
//        _dbContext = dbContext;
//    }

//    public async Task<TreeNode?> GetRootNodeByName(string name)
//    {
//        TreeNode? result = null;

//        //var tree = _dbContext.TreeNodes
//        //                    .Where(x => x.ParentId == 0 && x.Name == name)
//        //                    .Select(x => new TreeNode()
//        //                    {
//        //                        Id = x.Id,
//        //                        Text = x.Text,
//        //                        ParentId = x.ParentId,
//        //                        hierarchy = "0000" + c.Id,
//        //                        Children = GetChildren(categories, c.Id)
//        //                    })

//        return result;
//    }
//}
