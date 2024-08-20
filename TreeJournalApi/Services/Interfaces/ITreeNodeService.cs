using TreeJournalApi.Models;

namespace TreeJournalApi.Services.Interfaces
{
    public interface ITreeNodeService
    {
        Task<TreeNode> GetNodeByIdAsync(int id);
        Task<IEnumerable<TreeNode>> GetAllNodesAsync();
        Task<TreeNode> CreateNodeAsync(TreeNode node);
        Task<TreeNode> UpdateNodeAsync(TreeNode node);
        Task DeleteNodeAsync(int id);
    }
}