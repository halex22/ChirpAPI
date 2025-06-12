using ChirpAPI.Model;
using ChirpAPI.services.Model.DTOs;
using ChirpAPI.services.ViewModels;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChirpAPI.services.Services.Interfaces
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetAllComments();
        Task<List<Comment>> GetCommentsByChirpId(int chirpId);
        Task<Comment> CreateComment(CommentCreateDTO comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(int id);

    }
}
