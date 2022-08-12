using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweaterV1.Domain.Models;
using SweaterV1.Services.Services;

namespace SweaterV1.WebAPI.Controllers;

[ApiController]
public class LikeController : ControllerBase
{
    private readonly LikeService _likeService;

    public LikeController(LikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet("post/{postId}/likes")]
    public async Task<IEnumerable<LikeModelInformationDto>> GetListOfLikesByPost(int postId)
    {
        var like = await _likeService.GerListOfLikesByPost(postId);
        return like;
    }

    [HttpPost("post/like-create")]
    public async Task<LikeModelCreationDto> PostLike(LikeModelCreationDto like, int postId)
    {
        await _likeService.CreateLike(like, postId);
        return like;
    }

    [Authorize]
    [HttpDelete("like/delete")]
    public async Task DeleteLike(int likeId)
    {
        await _likeService.DeleteLike(likeId);
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("like/delete-all")]
    public async Task DeleteAllLikes(int postId)
    {
        await _likeService.DeleteAllLikes(postId);
    }
}