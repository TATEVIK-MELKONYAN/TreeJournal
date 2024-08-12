using TreeJournalApi.Models;

public class TreeService : ITreeService
{
    private readonly IRepository<TreeNode> _repository;

    public TreeService(IRepository<TreeNode> repository)
    {
        _repository = repository;
    }

    public async Task<TreeNode> GetNodeByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TreeNode>> GetAllNodesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task AddNodeAsync(TreeNode node)
    {
        await _repository.AddAsync(node);
    }

    public async Task UpdateNodeAsync(TreeNode node)
    {
        await _repository.UpdateAsync(node);
    }

    public async Task DeleteNodeAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
