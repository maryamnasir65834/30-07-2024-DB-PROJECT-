using BookStoreApi.DataAccessLayer;
using BookStoreApi.Models;
using System.Collections.Generic;

namespace BookStoreApi.Services
{
    public class CommentBL
    {
        private readonly CommentDAL _commentDAL;

        public CommentBL(CommentDAL commentDAL)
        {
            _commentDAL = commentDAL;
        }

        public List<Comment> GetComments()
        {
            return _commentDAL.GetComments();
        }

        public Comment GetComment(int id)
        {
            return _commentDAL.GetComment(id);
        }

        public void AddComment(Comment comment)
        {
            _commentDAL.AddComment(comment);
        }

        public void UpdateComment(int id, Comment comment)
        {
            var existingComment = _commentDAL.GetComment(id);
            if (existingComment != null)
            {
                comment.Id = id; // Ensure the ID remains unchanged
                _commentDAL.UpdateComment(comment);
            }
        }

        public void DeleteComment(int id)
        {
            _commentDAL.DeleteComment(id);
        }
    }
}
