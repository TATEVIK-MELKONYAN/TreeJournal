using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TreeNode
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    public long? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual TreeNode Parent { get; set; }

    public virtual ICollection<TreeNode> Children { get; set; } = new List<TreeNode>();
}
