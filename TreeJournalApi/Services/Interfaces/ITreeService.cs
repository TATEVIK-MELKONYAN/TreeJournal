using TreeJournalApi.Models;

public interface ITreeService
{
    Task<TreeNode> GetNodeByIdAsync(long id);
    Task<IEnumerable<TreeNode>> GetAllNodesAsync();
    Task AddNodeAsync(TreeNode node);
    Task UpdateNodeAsync(TreeNode node);
    Task DeleteNodeAsync(long id);
}
