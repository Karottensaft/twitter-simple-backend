using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.WebAPI.Controllers;

[ApiController]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [Authorize(Roles = "admin")]
    [HttpGet("comment/all")]
    public async Task<IEnumerable<CommentModel>> GetListAsync()
    {
        var comment = await _commentService.GerListOfEntities();
        return comment;
    }

    [HttpGet("comment/{commentId}")]
    public async Task<CommentModelInformationDto> GetAsync(int commentId)
    {
        var comment = await _commentService.GetEntity(commentId);
        return comment;
    }

    [HttpPost("user/{userId}/comment-create")]
    public async Task<CommentModelCreationDto> Post(CommentModelCreationDto comment)
    {
        await _commentService.CreateEntity(comment);
        return comment;
    }

    [HttpPut("comment/{commentId}/comment-change")]
    public async Task<CommentModelChangeDto> Put(CommentModelChangeDto comment, int commentId)
    {
        await _commentService.UpdateEntity(comment, commentId);
        return comment;
    }

    [HttpDelete("comment/{commentId}/delete")]
    public async Task Delete(int commentId)
    {
        await _commentService.DeleteEntity(commentId);
    }
}