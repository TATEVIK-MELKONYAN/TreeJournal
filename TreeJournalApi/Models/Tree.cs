using System.ComponentModel.DataAnnotations;

namespace TreeJournalApi.Models
{
    public class Tree
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<TreeNode> Nodes { get; set; } = new List<TreeNode>();
    }
}
