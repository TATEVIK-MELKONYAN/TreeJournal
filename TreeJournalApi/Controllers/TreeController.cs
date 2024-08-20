using Microsoft.AspNetCore.Mvc;
using TreeJournalApi.Models;
using TreeJournalApi.Services.Interfaces;

namespace TreeJournalApi.Controllers
{
    [ApiController]
    [Route("api.user.tree.node")]
    public class NodeController : ControllerBase
    {
        private readonly ITreeNodeService _nodeService;

        public NodeController(ITreeNodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNode([FromBody] TreeNode node)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdNode = await _nodeService.CreateNodeAsync(node);
            return CreatedAtAction(nameof(GetNode), new { id = createdNode.Id }, createdNode);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteNode([FromBody] int id)
        {
            try
            {
                await _nodeService.DeleteNodeAsync(id);
                return NoContent();
            }
            catch (SecureException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    type = "Secure",
                    id = DateTime.Now.Ticks,
                    data = new { message = ex.Message }
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    type = "Exception",
                    id = DateTime.Now.Ticks,
                    data = new { message = $"Internal server error ID = {DateTime.Now.Ticks}" }
                });
            }
        }

        [HttpPost("rename")]
        public async Task<IActionResult> RenameNode([FromBody] TreeNode node)
        {
            try
            {
                var updatedNode = await _nodeService.UpdateNodeAsync(node);
                return Ok(updatedNode);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    type = "Exception",
                    id = DateTime.Now.Ticks,
                    data = new { message = $"Internal server error ID = {DateTime.Now.Ticks}" }
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNode(int id)
        {
            try
            {
                var node = await _nodeService.GetNodeByIdAsync(id);
                return Ok(node);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    type = "Exception",
                    id = DateTime.Now.Ticks,
                    data = new { message = $"Internal server error ID = {DateTime.Now.Ticks}" }
                });
            }
        }
    }
}