using System.Data;
using Microsoft.Data.SqlClient;
using BookStoreApi.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreApi.DataAccessLayer
{
    public class CommentDAL
    {
        private readonly string _connectionString;

        public CommentDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Comment> GetComments()
        {
            var comments = new List<Comment>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllComments", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var comment = new Comment
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Subject = reader["Subject"].ToString(),
                            Message = reader["Message"].ToString()
                        };

                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public Comment GetComment(int id)
        {
            Comment comment = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetCommentsById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        comment = new Comment
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Subject = reader["Subject"].ToString(),
                            Message = reader["Message"].ToString()
                        };
                    }
                }
            }

            return comment;
        }

        public void AddComment(Comment comment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddComments", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Name", comment.Name);
                cmd.Parameters.AddWithValue("@Email", comment.Email);
                cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                cmd.Parameters.AddWithValue("@Message", comment.Message);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateComment(Comment comment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateComments", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", comment.Id);
                cmd.Parameters.AddWithValue("@Name", comment.Name);
                cmd.Parameters.AddWithValue("@Email", comment.Email);
                cmd.Parameters.AddWithValue("@Subject", comment.Subject);
                cmd.Parameters.AddWithValue("@Message", comment.Message);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteComment(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteComments", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
