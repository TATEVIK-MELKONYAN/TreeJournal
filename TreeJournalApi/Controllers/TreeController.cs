using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user/tree")]
public class TreeController : ControllerBase
{
    private readonly ITreeService _treeService;

    public TreeController(ITreeService treeService)
    {
        _treeService = treeService;
    }

    [HttpPost("node/create")]
    public async Task<IActionResult> CreateNode([FromBody] TreeNode node)
    {
        if (node == null || string.IsNullOrEmpty(node.Name))
        {
            return BadRequest("Invalid node data.");
        }

        await _treeService.AddNodeAsync(node);
        return CreatedAtAction(nameof(GetNode), new { id = node.Id }, node);
    }

    [HttpGet("node/{id}")]
    public async Task<IActionResult> GetNode(long id)
    {
        var node = await _treeService.GetNodeByIdAsync(id);
        if (node == null)
        {
            return NotFound();
        }

        return Ok(node);
    }

    [HttpDelete("node/delete/{id}")]
    public async Task<IActionResult> DeleteNode(long id)
    {
        var node = await _treeService.GetNodeByIdAsync(id);
        if (node == null)
        {
            return NotFound();
        }

        await _treeService.DeleteNodeAsync(id);
        return NoContent();
    }

    [HttpPut("node/rename/{id}")]
    public async Task<IActionResult> RenameNode(long id, [FromBody] string newName)
    {
        var node = await _treeService.GetNodeByIdAsync(id);
        if (node == null)
        {
            return NotFound();
        }

        node.Name = newName;
        await _treeService.UpdateNodeAsync(node);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetTree()
    {
        var nodes = await _treeService.GetAllNodesAsync();
        return Ok(nodes);
    }
}
