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
    public async Task<IEnumerable<LikeModelInformationDto>> GetListOfLikesAsyncByPost(int postId)
    {
        var like = await _likeService.GerListOfEntitiesByPost(postId);
        return like;
    }


    [HttpPost("post/{postId}/like-creator")]
    public async Task<LikeModelCreationDto> PostLike(LikeModelCreationDto like)
    {
        await _likeService.CreateEntity(like);
        return like;
    }

    [HttpDelete("like/{likeId}/delete")]
    public async Task DeleteLike(int likeId)
    {
        await _likeService.DeleteEntity(likeId);
    }

    [HttpDelete("like/{postId}/delete-all")]
    public async Task DeleteAllEntities(int postId)
    {
        await _likeService.DeleteAllEntities(postId);
    }
}