using ChirpAPI.Model;
using ChirpAPI.services.Model.DTOs;
using ChirpAPI.services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.Services
{
    public class HugoCommentService : ICommentsService
    {
        private readonly ChirpContext _context;

        public HugoCommentService(ChirpContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateComment(CommentCreateDTO rawComment)
        {            
            var chirp = await _context.Chirps.FindAsync(rawComment.ChirpId);
            if (chirp == null) throw new KeyNotFoundException($"Chirp with ID {rawComment.ChirpId} not found.");
            var comment = new Comment
            {
                ChirpId = rawComment.ChirpId,
                Text = rawComment.Text,
            };
            _context.Comments.Add(comment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return comment;

        }

        public async Task DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) throw new DBConcurrencyException($"Comment with ID {id} not found after creation.");
            _context.Comments.Remove(comment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the comment: {ex.Message}", ex);
            }


        }

        public async Task<List<CommentDTO>> GetAllComments()
        {
            return await _context.Comments
                .Select( c => new CommentDTO
                {
                    Id = c.Id,
                    Text = c.Text,
                    CreationDate = c.CreationDate,
                    ChirpId= c.ChirpId,
                    ParentId= c.ParentId,
                })
                .ToListAsync();
        }

        public Task<List<Comment>> GetCommentsByChirpId(int chirpId)
        {
            return _context.Comments
                .Where(c => c.ChirpId == chirpId)
                .ToListAsync();
        }

        public Task UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        private bool CommentExists(int commentId) => _context.Comments.Any(c => c.Id == commentId);
    }
}
