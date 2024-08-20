using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeJournalApi.Models
{
    public class TreeNode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TreeId { get; set; }

        [ForeignKey("TreeId")]
        public Tree Tree { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public TreeNode Parent { get; set; }

        public ICollection<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}