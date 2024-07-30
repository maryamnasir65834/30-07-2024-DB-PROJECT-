using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Models;
using BookStoreApi.Services;
using System;
using System.Collections.Generic;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentBL _commentBL;

        public CommentsController(CommentBL commentBL)
        {
            _commentBL = commentBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetComments()
        {
            var comments = _commentBL.GetComments();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            var comment = _commentBL.GetComment(id);
            if (comment == null)
                return NotFound(new { Message = "Comment not found" });
            return Ok(comment);
        }

        [HttpPost]
        public ActionResult<Comment> CreateComment([FromBody] Comment comment)
        {
            if (comment == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid comment data.");
            }

            _commentBL.AddComment(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromBody] Comment comment)
        {
            var existingComment = _commentBL.GetComment(id);
            if (existingComment == null)
                return NotFound(new { Message = "Comment not found" });

            comment.Id = id; // Ensure the ID remains unchanged
            _commentBL.UpdateComment(id, comment);
            return Ok(new { Message = "Comment updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _commentBL.GetComment(id);
            if (comment == null)
                return NotFound(new { Message = "Comment not found" });

            _commentBL.DeleteComment(id);
            return Ok(new { Message = "Comment deleted successfully" });
        }
    }
}
