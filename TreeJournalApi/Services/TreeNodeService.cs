using TreeJournalApi.Models;
using TreeJournalApi.Repositories.Interfaces;
using TreeJournalApi.Services.Interfaces;

namespace TreeJournalApi.Services
{
    public class TreeNodeService : ITreeNodeService
    {
        private readonly IRepository<TreeNode> _nodeRepository;

        public TreeNodeService(IRepository<TreeNode> nodeRepository)
        {
            _nodeRepository = nodeRepository;
        }

        public async Task<TreeNode> GetNodeByIdAsync(int id)
        {
            var node = await _nodeRepository.GetByIdAsync(id);
            return node ?? throw new ArgumentException("Node not found");
        }

        public async Task<IEnumerable<TreeNode>> GetAllNodesAsync()
        {
            return await _nodeRepository.GetAllAsync();
        }

        public async Task<TreeNode> CreateNodeAsync(TreeNode node)
        {
            await _nodeRepository.AddAsync(node);
            return node;
        }

        public async Task<TreeNode> UpdateNodeAsync(TreeNode node)
        {
            var existingNode = await _nodeRepository.GetByIdAsync(node.Id) ?? throw new ArgumentException("Node not found");
            existingNode.Name = node.Name;
            await _nodeRepository.UpdateAsync(existingNode);
            return existingNode;
        }

        public async Task DeleteNodeAsync(int id)
        {
            var node = await _nodeRepository.GetByIdAsync(id) ?? throw new ArgumentException("Node not found");
            if (node.Children.Any())
                throw new SecureException("You have to delete all children nodes first");

            await _nodeRepository.DeleteAsync(id);
        }
    }
}