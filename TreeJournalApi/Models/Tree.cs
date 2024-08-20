using System.ComponentModel.DataAnnotations;

namespace TreeJournalApi.Models
{
    public class Tree
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<TreeNode> Nodes { get; set; } = new List<TreeNode>();
    }
}
